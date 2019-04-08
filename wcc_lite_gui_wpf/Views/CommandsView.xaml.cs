﻿using System.Windows.Controls;
using w3tools.common;
using wcc.core;

namespace w3tools.UI.Views
{
    /// <summary>
    /// Interaction logic for UCCommands.xaml
    /// </summary>
    public partial class CommandsView : UserControl
    {

        
       



        public CommandsView()
        {

            InitializeComponent();           

        }

        private void _commandsListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic viewModel = DataContext;
            var senderAsList = ((ListView)sender);
            viewModel.CommandDoubleClick((WorkflowItem)senderAsList.SelectedValue);
        }
    }

}
