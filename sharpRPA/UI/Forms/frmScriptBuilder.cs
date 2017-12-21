using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace sharpRPA.UI.Forms
{


    public partial class frmScriptBuilder : Form
    //Form tracks the overall configuration and enables script editing, saving, and running
    //Features ability to add, drag/drop reorder commands
    {
        #region Instance and Form Events

        private ListViewItem rowSelectedForCopy { get; set; }
        private List<Core.Script.ScriptVariable> scriptVariables;
  
        private ImageList uiImages;
        private WebSocket4Net.WebSocket webSocket;
        private string webSocketConnectionID;
        public Core.ApplicationSettings appSettings;
        private string streamedXMLData { get; set; }
        private List<List<ListViewItem>> undoList;
        private int undoIndex = -1;
        public frmScriptBuilder()
        {
            InitializeComponent();
        }
        private string scriptFilePath;
        public string ScriptFilePath
        {
            get
            {
                return scriptFilePath;
            }
            set
            {
                scriptFilePath = value;
                UpdateWindowTitle();
            }
        }

        private void UpdateWindowTitle()
        {


            if (ScriptFilePath != null)
            {
                System.IO.FileInfo scriptFileInfo = new System.IO.FileInfo(ScriptFilePath);
                this.Text = "sharpRPA - Automation Script Builder (" + scriptFileInfo.Name + ")";
            }
            else
            {
                this.Text = "sharpRPA - Automation Script Builder";
            }

            if ((webSocketConnectionID != "") && (webSocketConnectionID != null))
            {
                this.Text = this.Text + "Connected to Coordinator [" + webSocketConnectionID + "]";
            }



        }

        private void frmScriptBuilder_Load(object sender, EventArgs e)
        {

            //create undo list
            undoList = new List<List<ListViewItem>>();

            //get app settings
            var appSettingClass = new Core.ApplicationSettings();
            appSettings = appSettingClass.GetOrCreateApplicationSettings();

            //get server setting preferences
            var serverSettings = appSettings.ServerSettings;


            //try to connect to server
            if ((serverSettings.ServerConnectionEnabled) && (serverSettings.ConnectToServerOnStartup))
            {
                CreateSocketConnection(serverSettings.ServerURL);
            }


            //create folder to store scripts
            var rpaScriptsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\sharpRPA\\My Scripts\\";
            if (!System.IO.Directory.Exists(rpaScriptsFolder))
            {
                UI.Forms.Supplemental.frmDialog userDialog = new UI.Forms.Supplemental.frmDialog("Would you like to create a folder to save your scripts in now? A script folder is required to save scripts generated with this application. The new script folder path would be '" + rpaScriptsFolder + "'.", "Unable to locate Script Folder!", UI.Forms.Supplemental.frmDialog.DialogType.YesNo, 0);

                if (userDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.Directory.CreateDirectory(rpaScriptsFolder);
                }
            }


            //no height for status bar
            tlpControls.RowStyles[3].Height = 0;

            //instantiate for script variables
            scriptVariables = new List<Core.Script.ScriptVariable>();

            //pnlHeader.BackColor = Color.FromArgb(255, 214, 88);

            //instantiate and populate display icons for commands
            uiImages = UI.Images.UIImageList();
            lstScriptActions.SmallImageList = uiImages;

            //get commands
            var groupedCommands = Core.Common.GetGroupedCommands();
            foreach (var cmd in groupedCommands)
            {

                var group = cmd.Key as Core.AutomationCommands.Attributes.ClassAttributes.Group;
                TreeNode newGroup = new TreeNode(group.groupName);
             

                foreach (var subcmd in cmd)
                {
                   
                    Core.AutomationCommands.ScriptCommand newCommand = (Core.AutomationCommands.ScriptCommand)Activator.CreateInstance(subcmd);
                    TreeNode subNode = new TreeNode(newCommand.SelectionName);
                    //subNode.ImageIndex = uiImages.Images.IndexOfKey(newCommand.GetType().Name);
                    newGroup.Nodes.Add(subNode);
                }


                tvCommands.Nodes.Add(newGroup);

            }

            //tvCommands.ImageList = uiImages;

      
        }
     
        private void frmScriptBuilder_Shown(object sender, EventArgs e)
        {
            Notify("Welcome! Press 'Add Command' to get started!");
            Notify("Note this is currently 'Alpha' Software!");
        }
        #endregion

        #region Button Events
        private void btnOpen_Click(object sender, EventArgs e)
        {

            //create script selector
            UI.Forms.Supplemental.frmScriptSelector frmSelector = new UI.Forms.Supplemental.frmScriptSelector();

            //if script was selected, then run the script
            if (frmSelector.ShowDialog() == DialogResult.OK)
            {


                try
                {

                    lstScriptActions.Items.Clear();

                    XmlSerializer serializer = new XmlSerializer(typeof(Core.Script.Script));

                    var xmlData = frmSelector.selectedScript;
                    System.IO.FileStream fs = new System.IO.FileStream(xmlData, System.IO.FileMode.Open);

                    XmlReader reader = XmlReader.Create(fs);
                    var deserializedData = (Core.Script.Script)serializer.Deserialize(reader);
                    fs.Close();

                    scriptVariables = deserializedData.Variables;

                    foreach (var cmdDetails in deserializedData.Commands)
                    {
                        AddCommandToListView(cmdDetails.ScriptCommand);
                    }

                    //txtFileName.Text = frmSelector.cboSelectFile.Text;
                    ScriptFilePath = xmlData;
                    Notify("Script Loaded Successfully!");
                }
                catch (Exception ex)
                {
                    Notify("Oops, an error occured: " + ex.Message);
                }

            }






        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            for (int i = 30; i > 0; i--)
            {
                tlpControls.RowStyles[3].Height = i;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            //if (lstScriptActions.Items.Count == 0)
            //{
            //    return;
            //}

            ////define default output path
            //if (this.ScriptFilePath == null)
            //{
            //    var fileName = Microsoft.VisualBasic.Interaction.InputBox("Please enter a file name (without extension)", "Enter File Name", "Default", -1, -1);

            //    if (fileName == string.Empty)
            //        return;

            //    var rpaScriptsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\sharpRPA\\My Scripts\\";
            //    this.ScriptFilePath = rpaScriptsFolder + fileName + ".xml";
            //}

            ////create fileStream
            //var fileStream = System.IO.File.Create(this.ScriptFilePath);

            //var script = new RPAScript();

            ////save variables to file

            //script.variables = scriptVariables;

            ////save listview tags to command list
            //foreach (ListViewItem commandItem in lstScriptActions.Items)
            //{
            //    script.scriptCommands.Add((AutomationCommands.ScriptCommand)commandItem.Tag);
            //}

            ////output to xml file
            //XmlSerializer serializer = new XmlSerializer(typeof(RPAScript));
            //serializer.Serialize(fileStream, script);
            //fileStream.Close();

            ////show success dialog
            //Notify("File has been saved successfully!");

        }
        private void btnNewCommand_Click(object sender, EventArgs e)
        {

            //bring up new command configuration form
            var newCommandForm = new UI.Forms.frmCommandEditor();
            newCommandForm.creationMode = UI.Forms.frmCommandEditor.CreationMode.Add;
            newCommandForm.scriptVariables = this.scriptVariables;
            //if a command was selected
            if (newCommandForm.ShowDialog() == DialogResult.OK)
            {
                //add to listview               
                AddCommandToListView(newCommandForm.selectedCommand);
            }

        }
        private void btnRun_Click(object sender, EventArgs e)
        {

            if (ScriptFilePath == null)
            {
                return;
            }

            ////create script selector
            //UI.Forms.frmScriptSelector frmSelector = new UI.Forms.frmScriptSelector();

            ////if script was selected, then run the script
            //if (frmSelector.ShowDialog() == DialogResult.OK)
            //{
            Notify("Running Script..");
            UI.Forms.frmScriptEngine newEngine = new UI.Forms.frmScriptEngine(ScriptFilePath, this);
            newEngine.Show();
            //  }  

        }
        #endregion

        #region ListView Events
        #region ListView DragDrop

        private void lstScriptActions_ItemDrag(object sender, ItemDragEventArgs e)
        {
            lstScriptActions.DoDragDrop(lstScriptActions.SelectedItems, DragDropEffects.Move);
        }

        private void lstScriptActions_DragEnter(object sender, DragEventArgs e)
        {
            int len = e.Data.GetFormats().Length - 1;
            int i;
            for (i = 0; i <= len; i++)
            {
                if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                {
                    //The data from the drag source is moved to the target.
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void lstScriptActions_DragDrop(object sender, DragEventArgs e)
        {

            //Return if the items are not selected in the ListView control.
            if (lstScriptActions.SelectedItems.Count == 0)
            {
                return;
            }

            CreateUndoSnapshot();

            //Returns the location of the mouse pointer in the ListView control.
            Point cp = lstScriptActions.PointToClient(new Point(e.X, e.Y));
            //Obtain the item that is located at the specified location of the mouse pointer.
            ListViewItem dragToItem = lstScriptActions.GetItemAt(cp.X, cp.Y);
            if (dragToItem == null)
            {
                return;
            }
            //Obtain the index of the item at the mouse pointer.
            int dragIndex = dragToItem.Index;


            //foreach (ListViewItem command in lstScriptActions.SelectedItems)
            //{

            //    if (command.Tag is Core.AutomationCommands.EndLoopCommand)
            //    {
            //        for (int i = 0; i < dragIndex; i++)
            //        {
            //            if (lstScriptActions.Items[i].Tag is Core.AutomationCommands.BeginLoopCommand)
            //            {

            //            }
            //        }


            //    }

            //}



            ListViewItem[] sel = new ListViewItem[lstScriptActions.SelectedItems.Count];
            for (int i = 0; i <= lstScriptActions.SelectedItems.Count - 1; i++)
            {
                sel[i] = lstScriptActions.SelectedItems[i];
            }
            for (int i = 0; i < sel.GetLength(0); i++)
            {
                //Obtain the ListViewItem to be dragged to the target location.
                ListViewItem dragItem = sel[i];
                int itemIndex = dragIndex;
                if (itemIndex == dragItem.Index)
                {
                    return;
                }
                if (dragItem.Index < itemIndex)
                    itemIndex++;
                else
                    itemIndex = dragIndex + i;
                //Insert the item at the mouse pointer.
                ListViewItem insertItem = (ListViewItem)dragItem.Clone();
                lstScriptActions.Items.Insert(itemIndex, insertItem);
                //Removes the item from the initial location while 
                //the item is moved to the new location.
                lstScriptActions.Items.Remove(dragItem);
                FormatCommandListView();
            }

        }

        #endregion

        #region ListView Copy, Paste, Edit, Delete
        private void lstScriptActions_KeyDown(object sender, KeyEventArgs e)
        {
            //delete from listview if required
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem itm in lstScriptActions.SelectedItems)
                {
                    lstScriptActions.Items.Remove(itm);
                }

                CreateUndoSnapshot();
                FormatCommandListView();
            }
            else if ((e.Control) && (e.KeyCode == Keys.C))
            {
                //if (lstScriptActions.SelectedItems.Count == 1)
                //{
                //    rowSelectedForCopy = lstScriptActions.SelectedItems[0];
                //}

                CopyRow();

            }
            else if ((e.Control) && (e.KeyCode == Keys.V))
            {

                //if (rowSelectedForCopy != null)
                //{
                //    Core.AutomationCommands.ScriptCommand duplicatedCommand = (Core.AutomationCommands.ScriptCommand)Core.Common.Clone(rowSelectedForCopy.Tag);
                //    lstScriptActions.Items.Insert(lstScriptActions.SelectedIndices[0], CreateScriptCommandListViewItem(duplicatedCommand));
                //    FormatCommandListView();
                //}

                PasteRow();

            }
            else if ((e.Control) && (e.KeyCode == Keys.Z))
            {
      
                UndoChange();
                
            }
            else if ((e.Control) && (e.KeyCode == Keys.R))
            {
              
                RedoChange();
              
            }


        }

        private void CopyRow()
        {
            if (lstScriptActions.SelectedItems.Count == 1)
            {
                rowSelectedForCopy = lstScriptActions.SelectedItems[0];
            }
        }

        private void PasteRow()
        {
       

            if (rowSelectedForCopy != null)
            {
                Core.AutomationCommands.ScriptCommand duplicatedCommand = (Core.AutomationCommands.ScriptCommand)Core.Common.Clone(rowSelectedForCopy.Tag);
                lstScriptActions.Items.Insert(lstScriptActions.SelectedIndices[0], CreateScriptCommandListViewItem(duplicatedCommand));
                FormatCommandListView();
            }

            CreateUndoSnapshot();

        }

        private void UndoChange()
        {

            if (undoList.Count > 0)
            {
     
                if ((undoIndex < 0) || (undoIndex >= undoList.Count))
                {
                    undoIndex = undoList.Count - 1;
                }

                
                lstScriptActions.Items.Clear();

                foreach (ListViewItem rowItem in undoList[undoIndex])
                {
                    lstScriptActions.Items.Add(rowItem);
                }

                undoIndex--;

                FormatCommandListView();

            }

        }

        private void RedoChange()
        {
            if (undoList.Count > 0)
            {
                
                undoIndex++;

                if (undoIndex > undoList.Count - 1)
                {
                    undoIndex = undoList.Count - 1;
                }

            

                lstScriptActions.Items.Clear();

                foreach (ListViewItem rowItem in undoList[undoIndex])
                {
                    lstScriptActions.Items.Add(rowItem);
                }

              
                FormatCommandListView();

   
        }

    }

            private void CreateUndoSnapshot()
        {

            
                   



            List<ListViewItem> itemList = new List<ListViewItem>();
            foreach (ListViewItem rowItem in lstScriptActions.Items)
            { 
                itemList.Add(rowItem);
            }

            undoList.Add(itemList);

            if (undoList.Count > 10)
            {
                undoList.RemoveAt(0);
            }


            undoIndex = itemList.Count - 1;



        }

        private void lstScriptActions_DoubleClick(object sender, EventArgs e)
        {
     
            if (lstScriptActions.SelectedItems.Count != 1)
            {
                return;
            }

            //bring up edit mode to edit the action
            ListViewItem selectedCommandItem = lstScriptActions.SelectedItems[0];

            //create new command editor form
            UI.Forms.frmCommandEditor editCommand = new UI.Forms.frmCommandEditor();

            //creation mode edit locks form to current command
            editCommand.creationMode = UI.Forms.frmCommandEditor.CreationMode.Edit;

            //set selected command from the listview item tag object which was assigned to the command
            var currentCommand = (Core.AutomationCommands.ScriptCommand)selectedCommandItem.Tag;

            //create clone of current command so databinding does not affect if changes are not saved
            editCommand.selectedCommand = Core.Common.Clone(currentCommand); ;

            //set variables
            editCommand.scriptVariables = this.scriptVariables;

            //show edit command form and save changes on OK result
            if (editCommand.ShowDialog() == DialogResult.OK)
            {
                selectedCommandItem.Tag = editCommand.selectedCommand;
                selectedCommandItem.Text = editCommand.selectedCommand.GetDisplayValue(); //+ "(" + cmdDetails.SelectedVariables() + ")";
                selectedCommandItem.SubItems.Add(editCommand.selectedCommand.GetDisplayValue());
            }

        }

        #endregion

        #region ListView Create Item

        private ListViewItem CreateScriptCommandListViewItem(Core.AutomationCommands.ScriptCommand cmdDetails)
        {
            ListViewItem newCommand = new ListViewItem();
            newCommand.Text = cmdDetails.GetDisplayValue(); //+ "(" + cmdDetails.SelectedVariables() + ")";
            newCommand.SubItems.Add(cmdDetails.GetDisplayValue());
            newCommand.Tag = cmdDetails;
            newCommand.ForeColor = cmdDetails.DisplayForeColor;

            newCommand.ImageIndex = uiImages.Images.IndexOfKey(cmdDetails.GetType().Name);
            return newCommand;
        }

        private void AddCommandToListView(Core.AutomationCommands.ScriptCommand selectedCommand)
        {
          

            lstScriptActions.Items.Add(CreateScriptCommandListViewItem(selectedCommand));

            if (selectedCommand is Core.AutomationCommands.BeginLoopCommand)
            {
                lstScriptActions.Items.Add(CreateScriptCommandListViewItem(new Core.AutomationCommands.EndLoopCommand()));
            }

            CreateUndoSnapshot();

            FormatCommandListView();
        }

        #endregion

        #region ListView Comment and Coloring
        private void FormatCommandListView()
        {

       

            if (pnlCommandHelper.Visible)
                pnlCommandHelper.Hide();


            int indent = 0;
            foreach (ListViewItem rowItem in lstScriptActions.Items)
            {

                if (rowItem.Tag is Core.AutomationCommands.BeginLoopCommand)
                {
                    indent += 2;
                    rowItem.IndentCount = indent;
                    indent += 2;
                }
                else if (rowItem.Tag is Core.AutomationCommands.EndLoopCommand)
                {
                    indent -= 2;
                    if (indent < 0) indent = 0;
                    rowItem.IndentCount = indent;
                    indent -= 2;
                    if (indent < 0) indent = 0;


                }
                else
                {
                    rowItem.IndentCount = indent;
                }




                //mod 2 to color alt rows
                if (rowItem.Index % 2 == 0)
                {
                    rowItem.BackColor = Color.White;
                }
                else
                {
                    rowItem.BackColor = Color.AliceBlue;
                }

                //if code is commented change back color
                var selectedCommand = (Core.AutomationCommands.ScriptCommand)rowItem.Tag;

                if ((selectedCommand.IsCommented) || (selectedCommand is Core.AutomationCommands.CommentCommand))
                {
                    rowItem.ForeColor = Color.ForestGreen;
                }
                else
                {
                    rowItem.ForeColor = selectedCommand.DisplayForeColor;
                }

                //handle pause before execution items
                if (selectedCommand.PauseBeforeExeucution)
                {
                    rowItem.BackColor = Color.LightYellow;
                    rowItem.ForeColor = Color.SteelBlue;
                }

                if (DebugLine > 0)
                {
                    lstScriptActions.Items[DebugLine - 1].BackColor = Color.OrangeRed;
                    lstScriptActions.Items[DebugLine - 1].ForeColor = Color.White;
                }





            }
        }

        private int debugLine;
        public int DebugLine
        {
            get
            {
                return debugLine;
            }
            set
            {
                debugLine = value;
                FormatCommandListView();
            }
        }
        private void lstScriptActions_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lstScriptActions.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    lstContextStrip.Show(Cursor.Position);
                }
            }


        }
        private void SetSelectedCodeToCommented(bool setCommented)
        {

            //warn if nothing was selected
            if (lstScriptActions.SelectedItems.Count == 0)
            {
                Notify("No code was selected!");
            }

            //get each item and set appropriately
            foreach (ListViewItem item in lstScriptActions.SelectedItems)
            {
                var selectedCommand = (Core.AutomationCommands.ScriptCommand)item.Tag;
                selectedCommand.IsCommented = setCommented;
            }

            //recolor
            FormatCommandListView();

            //clear selection
            lstScriptActions.SelectedIndices.Clear();

        }
        private void SetPauseBeforeExecution()
        {

            //warn if nothing was selected
            if (lstScriptActions.SelectedItems.Count == 0)
            {
                Notify("No code was selected!");
            }

            //get each item and set appropriately
            foreach (ListViewItem item in lstScriptActions.SelectedItems)
            {
                var selectedCommand = (Core.AutomationCommands.ScriptCommand)item.Tag;
                selectedCommand.PauseBeforeExeucution = !selectedCommand.PauseBeforeExeucution;
            }

            //recolor
            FormatCommandListView();

            //clear selection
            lstScriptActions.SelectedIndices.Clear();

        }
        private void disableSelectedCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSelectedCodeToCommented(true);
        }
        private void enableSelectedCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSelectedCodeToCommented(false);
        }
        private void pauseBeforeExecutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPauseBeforeExecution();
        }
        #endregion

        #endregion

        #region Bottom Notification Panel
        List<string> notificationList = new List<string>();
        private DateTime notificationExpires;
        private bool isDisplaying;
        private void tmrNotify_Tick(object sender, EventArgs e)
        {

            if ((notificationExpires < DateTime.Now) && (isDisplaying))
            {
                HideNotification();
            }



            //check if notification is required
            if ((notificationList.Count > 0) && (notificationExpires < DateTime.Now))
            {
                var itemToDisplay = notificationList[0];
                notificationList.RemoveAt(0);
                notificationExpires = DateTime.Now.AddSeconds(3);
                ShowNotification(itemToDisplay);
            }


        }
        public void Notify(string notificationText)
        {
            notificationList.Add(notificationText);
        }
        private void ShowNotification(string textToDisplay)
        {
            notificationText = textToDisplay;
            //lblStatus.Left = 20;
            //lblStatus.Text = textToDisplay;

            pnlStatus.SuspendLayout();
            //for (int i = 0; i < 30; i++)
            //{
            //    tlpControls.RowStyles[3].Height = i;
            //}
            tlpControls.RowStyles[3].Height = 30;
            pnlStatus.ResumeLayout();
            isDisplaying = true;
        }
        private void HideNotification()
        {

            pnlStatus.SuspendLayout();
            //for (int i = 30; i > 0; i--)
            //{
            //    tlpControls.RowStyles[3].Height = i;
            //}
            tlpControls.RowStyles[3].Height = 0;
            pnlStatus.ResumeLayout();

            isDisplaying = false;
        }
        public string notificationText { get; set; }
        private void pnlStatus_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(notificationText, pnlStatus.Font, Brushes.White, 30, 5);
            e.Graphics.DrawImage(Properties.Resources.message, 5, 5, 24, 24);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnManageVariables_Click(object sender, EventArgs e)
        {

            UI.Forms.frmScriptVariables scriptVariableEditor = new UI.Forms.frmScriptVariables();
            scriptVariableEditor.scriptVariables = this.scriptVariables;

            if (scriptVariableEditor.ShowDialog() == DialogResult.OK)
            {
                this.scriptVariables = scriptVariableEditor.scriptVariables;
            }

        }

        private void frmScriptBuilder_SizeChanged(object sender, EventArgs e)
        {
            lstScriptActions.Columns[0].Width = -2;
        }
        #endregion

        #region Open, Save, Parse File
        private void uiBtnOpen_Click(object sender, EventArgs e)
        {
            //create script selector
            UI.Forms.Supplemental.frmScriptSelector frmSelector = new UI.Forms.Supplemental.frmScriptSelector();

            //if script was selected, then run the script
            if (frmSelector.ShowDialog() == DialogResult.OK)
            {


                try
                {

                    lstScriptActions.Items.Clear();
                    scriptVariables = new List<Core.Script.ScriptVariable>();

                    this.ScriptFilePath = frmSelector.selectedScript;

                    Core.Script.Script deserializedScript = Core.Script.Script.DeserializeFile(ScriptFilePath);

                    if (deserializedScript.Commands.Count == 0)
                    {
                        Notify("Error Parsing File: Commands not found!");
                    }

                    scriptVariables = deserializedScript.Variables;

                    PopulateExecutionCommands(deserializedScript.Commands);

                    FormatCommandListView();

                    Notify("Script Loaded Successfully!");
                }
                catch (Exception ex)
                {
                    Notify("Oops, an error occured: " + ex.Message);
                }

            }
        }

        private void PopulateExecutionCommands(List<Core.Script.ScriptAction> commandDetails)
        {

            foreach (Core.Script.ScriptAction item in commandDetails)
            {
                lstScriptActions.Items.Add(CreateScriptCommandListViewItem(item.ScriptCommand));
                if (item.AdditionalScriptCommands.Count > 0) PopulateExecutionCommands(item.AdditionalScriptCommands);
            }


        }

        private void uiBtnSave_Click(object sender, EventArgs e)
        {

            if (lstScriptActions.Items.Count == 0)
            {
                return;
            }

            int loopValidationCount = 0;
            foreach (ListViewItem item in lstScriptActions.Items)
            {
                if (item.Tag is Core.AutomationCommands.BeginLoopCommand)
                {
                    loopValidationCount++;
                }
                else if (item.Tag is Core.AutomationCommands.EndLoopCommand)
                {
                    loopValidationCount--;
                }

                //end loop was found first
                if (loopValidationCount < 0)
                {
                    Notify("Please verify the ordering of your loops.");
                    return;
                }

            }

            //extras were found
            if (loopValidationCount != 0)
            {
                Notify("Please verify the ordering of your loops.");
                return;
            }



            //define default output path
            if (this.ScriptFilePath == null)
            {
                var fileName = Microsoft.VisualBasic.Interaction.InputBox("Please enter a file name (without extension)", "Enter File Name", "Default", -1, -1);

                if (fileName == string.Empty)
                    return;

                var rpaScriptsFolder = Core.Common.GetScriptFolderPath();

                if (!System.IO.Directory.Exists(rpaScriptsFolder))
                {
                    UI.Forms.Supplemental.frmDialog userDialog = new UI.Forms.Supplemental.frmDialog("Would you like to create a folder to save your scripts in now? A script folder is required to save scripts generated with this application. The new script folder path would be '" + rpaScriptsFolder + "'.", "Unable to locate Script Folder!", UI.Forms.Supplemental.frmDialog.DialogType.YesNo, 0);

                    if (userDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.Directory.CreateDirectory(rpaScriptsFolder);
                    }
                    else
                    {
                        return;
                    }


                }


                this.ScriptFilePath = rpaScriptsFolder + fileName + ".xml";
            }

            //serialize script
            try
            {
                var exportedScript = Core.Script.Script.SerializeScript(this.ScriptFilePath, lstScriptActions.Items, scriptVariables);
                //show success dialog
                Notify("File has been saved successfully!");
            }
            catch (Exception ex)
            {
                Notify("Error: " + ex.ToString());
            }




        }
        #endregion

        #region UI Buttons
        private void uiBtnAddCommand_Click(object sender, EventArgs e)
        {
            AddNewCommand();
        }

        private void uiBtnAddVariable_Click(object sender, EventArgs e)
        {
            UI.Forms.frmScriptVariables scriptVariableEditor = new UI.Forms.frmScriptVariables();
            scriptVariableEditor.scriptVariables = this.scriptVariables;

            if (scriptVariableEditor.ShowDialog() == DialogResult.OK)
            {
                this.scriptVariables = scriptVariableEditor.scriptVariables;
            }
        }

        private void uiBtnRunScript_Click(object sender, EventArgs e)
        {

            if (ScriptFilePath == null)
            {
                return;
            }

            Notify("Running Script..");
            UI.Forms.frmScriptEngine newEngine = new UI.Forms.frmScriptEngine(ScriptFilePath, this);
            newEngine.callBackForm = this;
            newEngine.Show();

        }

        private void uiBtnNew_Click(object sender, EventArgs e)
        {
            this.ScriptFilePath = null;
            lstScriptActions.Items.Clear();
            scriptVariables = new List<Core.Script.ScriptVariable>();
            pnlCommandHelper.Show();
        }

        private void uiBtnScheduleManagement_Click(object sender, EventArgs e)
        {
            UI.Forms.frmScheduleManagement scheduleManager = new UI.Forms.frmScheduleManagement();
            scheduleManager.Show();
        }

        private void uiBtnCommandExplorer_Click(object sender, EventArgs e)
        {

            UI.Forms.frmCommandBrowser cmdBrowser = new UI.Forms.frmCommandBrowser();
            if (cmdBrowser.ShowDialog() == DialogResult.OK)
            {
                //bring up new command configuration form
                var newCommandForm = new UI.Forms.frmCommandEditor();
                newCommandForm.creationMode = UI.Forms.frmCommandEditor.CreationMode.Add;
                newCommandForm.scriptVariables = this.scriptVariables;
                newCommandForm.defaultStartupCommand = cmdBrowser.lblCommandName.Text;
                //if a command was selected
                if (newCommandForm.ShowDialog() == DialogResult.OK)
                {
                    //add to listview       
                    AddCommandToListView(newCommandForm.selectedCommand);
                
                }


            }
        }

        private void uiBtnAbout_Click(object sender, EventArgs e)
        {
            UI.Forms.Supplemental.frmAbout frmAboutForm = new UI.Forms.Supplemental.frmAbout();
            frmAboutForm.Show();
        }

        private void uiBtnSettings_Click(object sender, EventArgs e)
        {
            //show settings dialog
            frmSettings newSettings = new frmSettings(this);
            newSettings.ShowDialog();

        }
        #endregion

        #region Create Command Logic
        private void AddNewCommand(string specificCommand = "")
        {
            //bring up new command configuration form
            var newCommandForm = new UI.Forms.frmCommandEditor();
            newCommandForm.creationMode = UI.Forms.frmCommandEditor.CreationMode.Add;
            newCommandForm.scriptVariables = this.scriptVariables;
            if (specificCommand != "")
                newCommandForm.defaultStartupCommand = specificCommand;
       
            //if a command was selected
            if (newCommandForm.ShowDialog() == DialogResult.OK)
            {
                //add to listview        
                AddCommandToListView(newCommandForm.selectedCommand);
            }

        }
        #endregion

        #region WebServer Socket Management

        public void CreateSocketConnection(string serverURL)
        {
            try
            {
                webSocket = new WebSocket4Net.WebSocket(serverURL);
                webSocket.Error += new EventHandler<ErrorEventArgs>(ConnectionError);
                webSocket.Opened += new EventHandler(ConnectionOpened);
                webSocket.Closed += new EventHandler(ConnectionClosed);
                webSocket.MessageReceived += new EventHandler<WebSocket4Net.MessageReceivedEventArgs>(websocket_MessageReceived);
                webSocket.Open();
            }
            catch (Exception ex)
            {
                Notify("Server Connection Problem: " + ex.Message);
            }
        }


        public void SendMessage(string message)
        {
            webSocket.Send(message);
        }

        private void ConnectionOpened(object sender, EventArgs e)
        {
            webSocket.Send("ClientStatus=Available");
        }
        private void ConnectionClosed(object sender, EventArgs e)
        {
            //start retrying to connect
        }

        private void ConnectionError(object sender, ErrorEventArgs e)
        {
            //connection has failed, start retrying
        }
        private void websocket_MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {


            if (e.Message.Contains("connectionID"))
            {
                //set connection ID to display locally
                webSocketConnectionID = e.Message;
                UpdateServerConnectionInfo(e.Message);
            }
            else if (e.Message.Contains("?xml"))
            {
                //code is being sent from the server so run it
                streamedXMLData = e.Message;
                RunXMLScript();
            }
        }

        private void UpdateServerConnectionInfo(string connectionID)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateServerConnectionInfo(connectionID);
                });

            }

            else
            {
                lblCoordinatorInfo.Text = "[connected to sharpRPAServer - " + connectionID + "]";
                lblCoordinatorInfo.Show();
            }

        }
        private void RunXMLScript()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(RunXMLScript));
            }
            else
            {
                frmScriptEngine newEngine = new UI.Forms.frmScriptEngine("", this);
                newEngine.xmlInfo = streamedXMLData;
                newEngine.callBackForm = this;
                newEngine.Show();
            }

        }

        #endregion

        #region TreeView Events
        private void tvCommands_DoubleClick(object sender, EventArgs e)
        {
            //exit if parent node is clicked
            if (tvCommands.SelectedNode.Parent == null)
            {
                return;
            }

            AddNewCommand(tvCommands.SelectedNode.Text);

        }
        #endregion

        private void pnlControlContainer_Paint(object sender, PaintEventArgs e)
        {

            Rectangle rect = new Rectangle(0, 0, pnlControlContainer.Width, pnlControlContainer.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.White, Color.WhiteSmoke, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }

            Pen steelBluePen = new Pen(Color.SteelBlue, 2);
            Pen lightSteelBluePen = new Pen(Color.LightSteelBlue, 1);
            //e.Graphics.DrawLine(steelBluePen, 0, 0, pnlControlContainer.Width, 0);
            e.Graphics.DrawLine(lightSteelBluePen, 0, 0, pnlControlContainer.Width, 0);
            e.Graphics.DrawLine(lightSteelBluePen, 0, pnlControlContainer.Height - 1, pnlControlContainer.Width, pnlControlContainer.Height - 1);

        }

        private void copySelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyRow();
        }

        private void pasteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteRow();
        }
    }
    }

