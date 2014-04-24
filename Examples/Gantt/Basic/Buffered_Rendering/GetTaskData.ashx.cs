using System;
using System.Collections.Generic;
using System.Web;
//using Task = Ext.Net.Gantt.Task;

namespace Ext.Net.Gantt.Demo.Examples.Buffered_Rendering
{
    /// <summary>
    /// Summary description for GetTaskData
    /// </summary>
    public class GetTaskData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NodeCollection arr = new NodeCollection();
            int i, j, k;
            List<Task> cn, cn2;
            Task tn;
            DateTime dt = new DateTime(2010, 1, 5);


            //for (i = 1; i < 10; i++)
            //{
            //    cn = new List<Task>();
            //    for (j = 1; j < 10; j++)
            //    {
            //        cn2 = new List<Task>();
            //        for (k = 1; k < 10; k++)
            //        {
            //            cn2.Add(new Task()
            //            {
            //                Id = 100 * i + 10 * j + k,
            //                Name = "Child task " + i.ToString() + j.ToString() + k.ToString(),
            //                StartDate = dt,
            //                Duration = 2,
            //                Leaf = true
            //            });
            //        }

            //        tn = new Task()
            //        {
            //            Id = 100 * i + 10 * j,
            //            Name = "Child task " + i.ToString() + j.ToString(),
            //            StartDate = dt,
            //            Duration = 2,
            //            Expanded = true
            //        };

            //        tn.Children.AddRange(cn2);
            //        cn.Add(tn);

            //        dt = dt.AddDays(7);
            //    }

            //    tn = new Task()
            //    {
            //        Id = 100 * i,
            //        Name = "Root task #" + i,
            //        StartDate = new DateTime(2010, 1, 5),
            //        EndDate = dt,
            //        Expanded = true
            //    };
            //    tn.Children.AddRange(cn);

            //    arr.Add(tn);
            //}

            context.Response.ContentType = "text/json";
            context.Response.Write(arr.ToJson());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}