﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using w3tools.Workflows;

namespace w3tools.UI.Views
{
    /// <summary>
    /// Interaction logic for WorkflowList.xaml
    /// </summary>
    public partial class WorkflowListView : UserControl
    {
        public WorkflowListView()
        {
            InitializeComponent();
        }

       

        private void _workflowsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic viewModel = DataContext;
            var senderAsList = ((ListView)sender);
            viewModel.CommandDoubleClick((RAD_Workflow)senderAsList.SelectedValue);
        }
    }
}
