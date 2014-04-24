<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            Scheduler1.Plugins.Add(new Zones
            {
                Store = new Store
                {
                    Model = 
                    {
                        new Model
                        {
                            Extend = "Sch.model.Range"
                        }
                    },
                        
                    Data = new List<object> 
                    { 
                        new 
                        {
                            StartDate = new DateTime(2011, 1, 27),
                            EndDate = new DateTime(2011, 1, 29),
                            Cls = "myZoneStyle"
                        }
                    }
                }
            });

            Scheduler1.Plugins.Add(new Lines
            {
                Store = new Store
                {
                    Model = 
                    { 
                        new Model
                        {
                            Name = "Line",
                                
                            Fields = 
                            {
                                new ModelField("Date", ModelFieldType.Date),
                                new ModelField("Text"),
                                new ModelField("Cls")
                            }
                        }
                    },

                    DataSource = new List<object> 
                    { 
                        new 
                        {
                            Date = new DateTime(2011, 1, 28, 12, 0, 0),
                            Text = "Some important date",
                            Cls = "important"
                        }
                    }
                }
            });
                
            this.CreateFakeData(Scheduler1);
        }
    }

    private void CreateFakeData(SchedulerGrid grid)
    {
        string[] firstNames = new string[] { "Russel", "Clark", "Steve", "Sam", "Lance", "Robert", "Sean", "David", "Justin", "Nicolas", "Brent" };            
        string[] lastNames = new string[] { "Wood", "Lewis", "Scott", "Parker", "Ross", "Garcia", "Bell", "Kelly", "Powell", "Moore", "Cook" };
        int[] salaries = new int[] { 100, 400, 900, 1500, 1000000 };
            
        var data = new List<object>();
        var random = new Random();
            
        for (int i = 0; i < 1000; i++) 
        {
            int salaryId    = random.Next(salaries.Length);
            int firstNameId = random.Next(firstNames.Length);
            int lastNameId = random.Next(lastNames.Length);

            data.Add(new {
                Id = i,
                index = i,
                salary = salaries[salaryId],
                name = string.Format("{0} {1}", firstNames[firstNameId], lastNames[lastNameId])
            });
        }
            
        grid.ResourceStore = new ResourceStore
        {
            Model = 
            {
                new Model
                {
                    Extend = "Sch.model.Resource",
                        
                    Fields = 
                    {
                        new ModelField("Id", ModelFieldType.Int),
                        new ModelField("index", ModelFieldType.Int),
                        new ModelField("salary", ModelFieldType.Float),
                        new ModelField("name")
                    }
                }
            },
                
            Data = data
        };

        var eventStore = new Ext.Net.Scheduler.EventStore();
            
        for (int i = 0; i < 1000; i++) 
        {                        
            eventStore.Events.Add(new Event
            {
                Id = "Event" + i,
                ResourceId = i,
                Name = "Event" + i + "-1",
                StartDate = new DateTime(2011, 1, 26),
                EndDate = new DateTime(2011, 1, 27)
            });

            if (i % 2 != 0)
            {
                eventStore.Events.Add(new Event
                {
                    Id = "Event" + i + "-2",
                    ResourceId = i,
                    Name = "Event" + i + "-2",
                    StartDate = new DateTime(2011, 1, 26),
                    EndDate = new DateTime(2011, 1, 28)
                });
            }
        }
            
        grid.EventStore = eventStore;
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Buffered view - Ext.Net.Scheduler Examples</title>    

    <style type="text/css">
        .myZoneStyle {
            background : whitesmoke url(pattern.png) repeat !important;
        }

        .important {
            border-left : 2px dotted red;    
        }
    </style>
</head>
<body style="padding:20px;">
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Buffered view</h1>

        <p>Here's a sample of how you can use a buffered view to display large datasets.</p>

        <sch:SchedulerGrid 
            ID="Scheduler1"
            runat="server"
            Width="700"
            Height="500"
            Title="Buffered view"
            StartDate="01.25.2011"
            EndDate="01.30.2011"
            DisableSelection="true"
            StandardViewPreset="DayAndWeek">
            <ColumnModel>
                <Columns>
                    <ext:Column 
                        runat="server" 
                        Text="#" 
                        Sortable="true" 
                        Width="50" 
                        DataIndex="index" />

                    <ext:Column 
                        runat="server" 
                        Text="Name" 
                        Sortable="true" 
                        Width="100" 
                        DataIndex="name" />

                    <ext:Column 
                        runat="server" 
                        Text="Salary" 
                        Sortable="true" 
                        Width="60" 
                        DataIndex="salary" />
                </Columns>
            </ColumnModel>

            <View>
                <sch:SchedulerGridView runat="server" TrackOver="false" />
            </View>

            <Plugins>
                <ext:BufferedRenderer runat="server" />
            </Plugins>
        </sch:SchedulerGrid>
    </form>
</body>
</html>