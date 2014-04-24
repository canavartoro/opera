<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    public class FavouriteColorResource : Resource
    {
        /// <summary>
        /// 
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        [Newtonsoft.Json.JsonProperty(DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual string FavoriteColor
        {
            get;
            set;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            SchedulerGrid1.ResourceStore.Resources.AddRange(new []{
                new FavouriteColorResource {Id = "MadMike", Name = "Mike", FavoriteColor = "blue"},
                new FavouriteColorResource {Id = "LindaAnderson", Name = "Linda", FavoriteColor = "red"},
                new FavouriteColorResource {Id = "DonJohnson", Name = "Don", FavoriteColor = "yellow"},
                new FavouriteColorResource {Id = "KarenJohnson", Name = "Karen", FavoriteColor = "black"},
                new FavouriteColorResource {Id = "DougHendricks", Name = "Doug", FavoriteColor = "green"},
                new FavouriteColorResource {Id = "PeterPan", Name = "Peter", FavoriteColor = "lime"}
            });

            SchedulerGrid1.EventStore = new Ext.Net.Scheduler.EventStore
            {
                Events = 
                { 
                    new Event { ResourceId= "MadMike",        Name = "Assignment 1", StartDate = new DateTime(2010, 12, 09, 10, 0, 0), EndDate = new DateTime(2010, 12, 09, 11, 0, 0)},
                    new Event { ResourceId= "LindaAnderson",  Name = "Assignment 2", StartDate = new DateTime(2010, 12, 09, 10, 0, 0), EndDate = new DateTime(2010, 12, 09, 12, 0, 0)},
                    new Event { ResourceId= "DonJohnson",     Name = "Assignment 3", StartDate = new DateTime(2010, 12, 09, 13, 0, 0), EndDate = new DateTime(2010, 12, 09, 15, 0, 0)},
                    new Event { ResourceId= "KarenJohnson",   Name = "Assignment 4", StartDate = new DateTime(2010, 12, 09, 13, 0, 0), EndDate = new DateTime(2010, 12, 09, 15, 0, 0)},
                    new Event { ResourceId= "DougHendricks",  Name = "Assignment 5", StartDate = new DateTime(2010, 12, 09, 12, 0, 0), EndDate = new DateTime(2010, 12, 09, 13, 0, 0)},
                    new Event { ResourceId= "PeterPan",       Name = "Assignment 6", StartDate = new DateTime(2010, 12, 09, 14, 0, 0), EndDate = new DateTime(2010, 12, 09, 16, 0, 0)}
                }
            };

            SchedulerGrid2.ResourceStore.Resources.AddRange(new[]{
                new FavouriteColorResource {Id = "r1", Name = "Mike", FavoriteColor = "blue"},
                new FavouriteColorResource {Id = "r2", Name = "Linda", FavoriteColor = "red"},
                new FavouriteColorResource {Id = "r3", Name = "Don", FavoriteColor = "yellow"},
                new FavouriteColorResource {Id = "r4", Name = "Karen", FavoriteColor = "black"},
                new FavouriteColorResource {Id = "r5", Name = "Doug", FavoriteColor = "green"},
                new FavouriteColorResource {Id = "r6", Name = "Peter", FavoriteColor = "lime"}
            });

            SchedulerGrid2.EventStore = new Ext.Net.Scheduler.EventStore
            {
                Events = 
                { 
                    new Event { ResourceId= "r1",  Name = "Assignment 1", StartDate = new DateTime(2010, 12, 06), EndDate = new DateTime(2010, 12, 07)},
                    new Event { ResourceId= "r2",  Name = "Assignment 2", StartDate = new DateTime(2010, 12, 07), EndDate = new DateTime(2010, 12, 09)},
                    new Event { ResourceId= "r3",  Name = "Assignment 3", StartDate = new DateTime(2010, 12, 08), EndDate = new DateTime(2010, 12, 12)},
                    new Event { ResourceId= "r4",  Name = "Assignment 4", StartDate = new DateTime(2010, 12, 07), EndDate = new DateTime(2010, 12, 11)},
                    new Event { ResourceId= "r5",  Name = "Assignment 5", StartDate = new DateTime(2010, 12, 09), EndDate = new DateTime(2010, 12, 18)},
                    new Event { ResourceId= "r6",  Name = "Assignment 6", StartDate = new DateTime(2010, 12, 18), EndDate = new DateTime(2010, 12, 20)}
                }
            };
            
            SchedulerGrid1.StartDate = new DateTime(2010, 12, 9, 8, 0, 0);
            SchedulerGrid1.EndDate = new DateTime(2010, 12, 9, 16, 0, 0);

            SchedulerGrid2.StartDate = new DateTime(2010, 12, 6);
            SchedulerGrid2.EndDate = new DateTime(2010, 12, 13);
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Column position - Ext.Net.Scheduler Examples</title>        

    <style type="text/css">
        .sch-schedulerpanel .x-grid-cell:not(.sch-timetd)
        {
            background: none repeat scroll 0 0 #CCEEEE !important;
            border-bottom-color: lightBlue;
            border-top-color: #FFEEEE;
        }
    </style>
</head>
<body style="padding:20px;">
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Column position</h1>
        <p>This example highlights the column position options you have. You can choose 'position' to be 'left' (default) or 'right'.</p>

        <ext:Model runat="server" 
            Name="App.CustomResource"
            Extend="Sch.model.Resource">
            <Fields>
                <ext:ModelField Name="FavoriteColor" />
            </Fields>
        </ext:Model>

        <sch:SchedulerGrid ID="SchedulerGrid1" runat="server"
            Title="Both columns to the left"
            EventBarTextField="Title"
            StandardViewPreset="HourAndDay"
            Height="200"
            Width="800">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" Text="Add new row" Handler="var s1 = this.up('schedulergrid');s1.resourceStore.insert(0, new s1.resourceStore.model({Name : 'New guy', FavoriteColor : 'black'}));" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel>
                <Columns>
                    <ext:Column runat="server" Text="Name" DataIndex="Name" Width="110" />
                    <ext:Column runat="server" Text="Favorite Color" DataIndex="FavoriteColor" Width="100" />
                </Columns>
            </ColumnModel>
            <ResourceStore ModelName="App.CustomResource">                
            </ResourceStore>
        </sch:SchedulerGrid>

        <sch:SchedulerGrid ID="SchedulerGrid2" runat="server"
            Title="One left, one right"
            EventBarTextField="Title"
            StandardViewPreset="DayAndWeek"
            Height="200"
            Width="800" 
            ForceFit="true">                       
            <ColumnModel>
                <Columns>
                    <ext:Column runat="server" Text="Name" DataIndex="Name" Width="130" />
                    <ext:Column runat="server" Text="Favorite Color" DataIndex="FavoriteColor" Width="100" Position="right" />
                </Columns>
            </ColumnModel>
            <ResourceStore ModelName="App.CustomResource">                
            </ResourceStore>
        </sch:SchedulerGrid>
    </form>
</body>
</html>