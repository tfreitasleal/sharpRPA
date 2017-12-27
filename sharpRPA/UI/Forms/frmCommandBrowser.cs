//Copyright (c) 2017 Jason Bayldon
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sharpRPA.UI.Forms
{
    public partial class frmCommandBrowser : UIForm
    {
        public frmCommandBrowser()
        {
            InitializeComponent();
        }
        Dictionary<string, Image> uiImages;
        List<IGrouping<Attribute, Type>> groupedCommands;
        private void frmCommandSelector_Load(object sender, EventArgs e)
        {
            uiImages = UI.Images.UIImageDictionary();

            groupedCommands = Assembly.GetExecutingAssembly().GetTypes()
                       .Where(t => t.Namespace == "sharpRPA.Core.AutomationCommands")
                       .Where(t => t.Name != "ScriptCommand")
                       .Where(t => t.IsAbstract == false)
                       .Where(t => t.BaseType.Name == "ScriptCommand")
                       .Where(t => CommandEnabled(t))
                       .GroupBy(t => t.GetCustomAttribute(typeof(Core.AutomationCommands.Attributes.ClassAttributes.Group)))
                       .ToList();

            foreach (var cmds in groupedCommands)
            {
                string assignedGroup = FindAssignedGroup(cmds);
                sharpRPA.UI.CustomControls.CommandGroupControl newFolderGroupControl = new sharpRPA.UI.CustomControls.CommandGroupControl();
                newFolderGroupControl.Click += CommandSelection_Click;
                newFolderGroupControl.GroupName = assignedGroup;
                flwCommandCategories.Controls.Add(newFolderGroupControl);
            }
        }

        private string FindAssignedGroup(IGrouping<Attribute, Type> cmd)
        {
            if (cmd.Key == null)
            {
                return "Unassigned";
            }
            else
            {
                var group = cmd.Key as Core.AutomationCommands.Attributes.ClassAttributes.Group;
                return group.groupName;
            }
        }
        private string FindFunctionalDescription(Type cmd)
        {
            Type cmdType = Type.GetType(cmd.FullName);
            var attribute = cmdType.GetCustomAttribute(typeof(Core.AutomationCommands.Attributes.ClassAttributes.Description), true) as Core.AutomationCommands.Attributes.ClassAttributes.Description;
            return attribute.commandFunctionalDescription;
        }
        private string FindImplementationDescription(Type cmd)
        {
            Type cmdType = Type.GetType(cmd.FullName);
            var attribute = cmdType.GetCustomAttribute(typeof(Core.AutomationCommands.Attributes.ClassAttributes.ImplementationDescription), true) as Core.AutomationCommands.Attributes.ClassAttributes.ImplementationDescription;
            return attribute.commandImplementationDescription;
        }

        private void CommandSelection_Click(object sender, EventArgs e)
        {
            while (flwCommandSelections.Controls.Count > 0) flwCommandSelections.Controls.RemoveAt(0);

            sharpRPA.UI.CustomControls.CommandGroupControl selectedControl = (sharpRPA.UI.CustomControls.CommandGroupControl)sender;

            var availableCommands = groupedCommands.Where(cmd => FindAssignedGroup(cmd) == selectedControl.GroupName).FirstOrDefault();

            foreach (var cmd in availableCommands)
            {
                //Instantiate Class
                Core.AutomationCommands.ScriptCommand newCommand = (Core.AutomationCommands.ScriptCommand)Activator.CreateInstance(cmd);

                sharpRPA.UI.CustomControls.CommandItemControl newitm = new sharpRPA.UI.CustomControls.CommandItemControl();
                if (uiImages.ContainsKey(newCommand.CommandName))
                {
                    newitm.CommandImage = uiImages[newCommand.CommandName];
                }

                newitm.CommandDisplay = newCommand.SelectionName;
                newitm.FunctionalDescription = FindFunctionalDescription(cmd);
                newitm.ImplementationDescription = FindImplementationDescription(cmd);
                newitm.ForeColor = Color.White;
                newitm.Click += CommandItem_Click;
                flwCommandSelections.Controls.Add(newitm);
            }
        }
        private void CommandItem_Click(object sender, EventArgs e)
        {
            if (!flwCommandDescription.Visible) flwCommandDescription.Visible = true;
            sharpRPA.UI.CustomControls.CommandItemControl selectedControl = (sharpRPA.UI.CustomControls.CommandItemControl)sender;
            lblCommandName.Text = selectedControl.CommandDisplay;
            lblFunctionalDescription.Text = selectedControl.FunctionalDescription;
            lblImplementationDescription.Text = selectedControl.ImplementationDescription;
        }

        private void uiBtnAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void uiBtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public static bool CommandEnabled(Type cmd)
        {
            var scriptCommand = (Core.AutomationCommands.ScriptCommand)Activator.CreateInstance(cmd);
            return scriptCommand.CommandEnabled;
        }

        private void flwCommandDescription_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}