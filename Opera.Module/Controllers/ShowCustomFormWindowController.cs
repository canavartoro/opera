using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.Controllers
{
    public abstract class ShowCustomFormWindowController : WindowController
    {
        private ShowNavigationItemController navigationController;
        public ShowCustomFormWindowController()
        {
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            navigationController = Frame.GetController<ShowNavigationItemController>();
            if (navigationController != null)
                navigationController.CustomShowNavigationItem += navigationController_CustomShowNavigationItem;
        }
        protected override void OnDeactivated()
        {
            if (navigationController != null)
                navigationController.CustomShowNavigationItem -= navigationController_CustomShowNavigationItem;
            base.OnDeactivated();
        }
        private void navigationController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            if (e.ActionArguments.SelectedChoiceActionItem.Id.StartsWith("CustomForm"))
            {
                ShowCustomForm(e.ActionArguments.SelectedChoiceActionItem.Model as IModelNavigationItem);
                e.Handled = true;
            }
        }
        protected abstract void ShowCustomForm(IModelNavigationItem model);
    }
}
