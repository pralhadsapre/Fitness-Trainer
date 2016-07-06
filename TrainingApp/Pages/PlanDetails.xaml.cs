using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TrainingApp.Models;

namespace TrainingApp.Pages
{
    public partial class PlanDetails : PhoneApplicationPage
    {
        public PlanDetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PlanDetailsContainer.DataContext = App.plansInfo.ElementAt(int.Parse(NavigationContext.QueryString["index"]));
        }

        private void OnTapApplyPlan(object sender, EventArgs e)
        {
            App.planSelected = (PlanInfo)PlanDetailsContainer.DataContext;
            NavigationService.Navigate(new Uri("/Pages/CurrentPlanPage.xaml", UriKind.Relative));
        }
    }
}