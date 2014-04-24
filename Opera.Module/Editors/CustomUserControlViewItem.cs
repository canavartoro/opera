using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Utils;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mikrobar.Module.Editors
{
    public interface IModelCustomUserControlViewItem : IModelViewItem
    {
    }

    [ViewItem(typeof(IModelCustomUserControlViewItem))]
    public abstract class CustomUserControlViewItem : ViewItem, IComplexViewItem
    {
        public CustomUserControlViewItem(IModelViewItem model, Type objectType)
            : base(objectType, model != null ? model.Id : string.Empty)
        {
        }
        private IObjectSpace theObjectSpace;
        private XafApplication theApplication;
        public IObjectSpace ObjectSpace
        {
            get { return theObjectSpace; }
        }
        public XafApplication Application
        {
            get { return theApplication; }
        }
        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            theObjectSpace = objectSpace;
            theApplication = application;
        }
        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            XpoSessionAwareControlInitializer.Initialize(Control as IXpoSessionAwareControl, theObjectSpace);
        }
    }

    public interface IXpoSessionAwareControl
    {
        void UpdateDataSource(Session session);
    }
    public static class XpoSessionAwareControlInitializer
    {
        public static void Initialize(IXpoSessionAwareControl control, IObjectSpace objectSpace)
        {
            // The IXpoSessionAwareControl interface is needed to pass a Session into a ModelDefault control that is supposed to implement this interface.
            //Guard.ArgumentNotNull(control, "control");
            //Guard.ArgumentNotNull(objectSpace, "objectSpace");

            // If a ModelDefault control is XAF-aware, then use the IObjectSpace to query data and bind it to your ModelDefault control (http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppBaseObjectSpacetopic). 
            // See some examples below:
            Type persistentDataType = typeof(DevExpress.Persistent.BaseImpl.Task);
            IList persistentData = objectSpace.GetObjects(persistentDataType, CriteriaOperator.Parse("Status = 'InProgress'"));

            // Session is required to query data when a ModelDefault control is XPO-aware only. 
            // You can pass an XafApplication into your ModelDefault control in a similar manner, if necessary.
            DevExpress.ExpressApp.Xpo.XPObjectSpace xpObjectSpace = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)objectSpace);
            if (control != null)
            {
                control.UpdateDataSource(xpObjectSpace.Session);

                // It is required to update the session when ObjectSpace is reloaded.
                objectSpace.Reloaded += delegate(object sender, EventArgs args)
                {
                    control.UpdateDataSource(xpObjectSpace.Session);
                };
            }
        }
        public static void Initialize(IXpoSessionAwareControl sessionAwareControl, XafApplication theApplication)
        {
            Initialize(sessionAwareControl, theApplication != null ? theApplication.CreateObjectSpace() : null);
        }
    }
}
