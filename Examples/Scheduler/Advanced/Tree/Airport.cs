using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext.Net.Scheduler.Demo
{
    public class Airport
    {
        public class SchTreeResource : Node
        {
            public override ConfigOptionsCollection ConfigOptions
            {
                get
                {
                    ConfigOptionsCollection list = base.ConfigOptions;
                    list.Add("id", new ConfigOption("id", new SerializationOptions("Id"), int.MinValue, this.Id));
                    list.Add("name", new ConfigOption("name", new SerializationOptions("Name"), null, this.Name));
                    list.Add("capacity", new ConfigOption("capacity", new SerializationOptions("Capacity"), int.MinValue, this.Capacity));

                    return list;
                }
            }

            [ConfigOption("Id")]
            [System.ComponentModel.DefaultValue(int.MinValue)]
            public virtual int Id
            {
                get
                {
                    return this.State.Get<int>("Id", int.MinValue);
                }
                set
                {
                    this.State.Set("Id", value);
                }
            }

            [ConfigOption("Name")]
            [System.ComponentModel.DefaultValue(null)]
            public virtual string Name
            {
                get
                {
                    return this.State.Get<string>("Name", null);
                }
                set
                {
                    this.State.Set("Name", value);
                }
            }

            [ConfigOption]
            [System.ComponentModel.DefaultValue(int.MinValue)]
            public virtual int Capacity
            {
                get
                {
                    return this.State.Get<int>("Capacity", int.MinValue);
                }
                set
                {
                    this.State.Set("Capacity", value);
                }
            }
        }
        
        public static NodeCollection Events
        {
            get
            {
                return new NodeCollection { 
                    new SchTreeResource {
                        Id = 0, 
                        Children = {
                            new SchTreeResource{
                                Id = 1,
                                Name = "Kastrup Airport",
                                IconCls = "sch-airport",
                                Expanded = true,
                                Children = {
                                    new SchTreeResource{
                                        Id = 2,
                                        Name = "Terminal A",
                                        IconCls = "sch-terminal",
                                        Expanded = true,
                                        Children = {
                                            new SchTreeResource{
                                                Id = 3,
                                                Name = "Gates 1 - 5",
                                                IconCls = "sch-gates-bundle",
                                                Expanded = true,
                                                Children = {
                                                    new SchTreeResource{
                                                        Id = 4,
                                                        Name = "Gate 1",
                                                        IconCls = "sch-gate",
                                                        Capacity = 100,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 5,
                                                        Name = "Gate 2",
                                                        IconCls = "sch-gate",
                                                        Capacity = 45,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 6,
                                                        Name = "Gate 3",
                                                        IconCls = "sch-gate",
                                                        Capacity = 45,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 7,
                                                        Name = "Gate 4",
                                                        IconCls = "sch-gate",
                                                        Capacity = 65,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 8,
                                                        Name = "Gate 5",
                                                        IconCls = "sch-gate",
                                                        Capacity = 75,
                                                        Leaf = true
                                                    }
                                                }
                                            },
                                            
                                            new SchTreeResource{
                                                Id = 9,
                                                Name = "Gates 6 - 10",
                                                IconCls = "sch-gates-bundle",
                                                Expanded = true,
                                                Children = {
                                                    new SchTreeResource{
                                                        Id = 10,
                                                        Name = "Gate 6",
                                                        IconCls = "sch-gate",
                                                        Capacity = 77,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 11,
                                                        Name = "Gate 7",
                                                        IconCls = "sch-gate",
                                                        Capacity = 85,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 12,
                                                        Name = "Gate 8",
                                                        IconCls = "sch-gate",
                                                        Capacity = 95,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 13,
                                                        Name = "Gate 9",
                                                        IconCls = "sch-gate",
                                                        Capacity = 55,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 14,
                                                        Name = "Gate 10",
                                                        IconCls = "sch-gate",
                                                        Capacity = 15,
                                                        Leaf = true
                                                    }
                                                }
                                            }   
                                        }
                                    },
                                    
                                    new SchTreeResource{
                                        Id = 15,
                                        Name = "Terminal B",
                                        IconCls = "sch-terminal",                                        
                                        Children = {
                                            new SchTreeResource{
                                                Id = 16,
                                                Name = "Gates 1 - 5",
                                                IconCls = "sch-gates-bundle",
                                                Children = {
                                                    new SchTreeResource{
                                                        Id = 17,
                                                        Name = "Gate 1",
                                                        IconCls = "sch-gate",
                                                        Capacity = 15,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 18,
                                                        Name = "Gate 2",
                                                        IconCls = "sch-gate",
                                                        Capacity = 45,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 19,
                                                        Name = "Gate 3",
                                                        IconCls = "sch-gate",
                                                        Capacity = 45,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 20,
                                                        Name = "Gate 4",
                                                        IconCls = "sch-gate",
                                                        Capacity = 65,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 21,
                                                        Name = "Gate 5",
                                                        IconCls = "sch-gate",
                                                        Capacity = 70,
                                                        Leaf = true
                                                    }
                                                }
                                            },
                                            
                                            new SchTreeResource{
                                                Id = 22,
                                                Name = "Gates 6 - 10",
                                                IconCls = "sch-gates-bundle",
                                                Children = {
                                                    new SchTreeResource{
                                                        Id = 23,
                                                        Name = "Gate 6",
                                                        IconCls = "sch-gate",
                                                        Capacity = 80,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 24,
                                                        Name = "Gate 7",
                                                        IconCls = "sch-gate",
                                                        Capacity = 120,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 25,
                                                        Name = "Gate 8",
                                                        IconCls = "sch-gate",
                                                        Capacity = 125,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 26,
                                                        Name = "Gate 9",
                                                        IconCls = "sch-gate",
                                                        Capacity = 100,
                                                        Leaf = true
                                                    },
                                                    
                                                    new SchTreeResource{
                                                        Id = 27,
                                                        Name = "Gate 10",
                                                        IconCls = "sch-gate",
                                                        Capacity = 100,
                                                        Leaf = true
                                                    }
                                                }
                                            }   
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }

        public static System.Collections.Generic.List<object> Resources
        {
            get
            {
                return new List<object>();
                //return new System.Collections.Generic.List<Ext.Net.Scheduler.Event> 
                //{ 
                //    new Ext.Net.Scheduler.Event{ ResourceId = 3, Name = "Summary", StartDate = new DateTime(2011, 12, 2, 8, 20,0), EndDate = new DateTime(2011, 12, 2, 11, 25, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 3, Name = "Summary", StartDate = new DateTime(2011, 12, 2, 12, 10,0), EndDate = new DateTime(2011, 12, 2, 13, 50, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 3, Name = "Summary", StartDate = new DateTime(2011, 12, 2, 14, 30,0), EndDate = new DateTime(2011, 12, 2, 16, 10, 0)},

                //    new Ext.Net.Scheduler.Event{ ResourceId = 6, Name = "London 895", StartDate = new DateTime(2011, 12, 2, 8, 20,0), EndDate = new DateTime(2011, 12, 2, 9, 50, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 4, Name = "Moscow 167", StartDate = new DateTime(2011, 12, 2, 9, 10,0), EndDate = new DateTime(2011, 12, 2, 10, 40, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 5, Name = "Berlin 291", StartDate = new DateTime(2011, 12, 2, 9, 25,0), EndDate = new DateTime(2011, 12, 2, 11, 25, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 7, Name = "Brussel 107", StartDate = new DateTime(2011, 12, 2, 12, 10,0), EndDate = new DateTime(2011, 12, 2, 13, 50, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 8, Name = "Krasnodar 101", StartDate = new DateTime(2011, 12, 2, 14, 30,0), EndDate = new DateTime(2011, 12, 2, 16, 10, 0)},

                //    new Ext.Net.Scheduler.Event{ ResourceId = 17, Name = "Split 811", StartDate = new DateTime(2011, 12, 2, 16, 10,0), EndDate = new DateTime(2011, 12, 2, 18, 30, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 18, Name = "Rome 587", StartDate = new DateTime(2011, 12, 2, 13, 15,0), EndDate = new DateTime(2011, 12, 2, 14, 25, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 24, Name = "Praga 978", StartDate = new DateTime(2011, 12, 2, 16, 40,0), EndDate = new DateTime(2011, 12, 2, 18, 00, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 25, Name = "Stockholm 581", StartDate = new DateTime(2011, 12, 2, 11, 10,0), EndDate = new DateTime(2011, 12, 2, 12, 30, 0)},

                //    new Ext.Net.Scheduler.Event{ ResourceId = 10, Name = "Copenhagen 111", StartDate = new DateTime(2011, 12, 2, 16, 10,0), EndDate = new DateTime(2011, 12, 2, 18, 30, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 11, Name = "Gothenburg 233", StartDate = new DateTime(2011, 12, 2, 13, 15,0), EndDate = new DateTime(2011, 12, 2, 14, 25, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 12, Name = "New York 231", StartDate = new DateTime(2011, 12, 2, 16, 40,0), EndDate = new DateTime(2011, 12, 2, 18, 00, 0)},
                //    new Ext.Net.Scheduler.Event{ ResourceId = 13, Name = "Paris 321", StartDate = new DateTime(2011, 12, 2, 11, 10,0), EndDate = new DateTime(2011, 12, 2, 12, 30, 0)}
                //};    
            }
        }
    }
}