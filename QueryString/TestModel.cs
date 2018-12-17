using System;
using System.Collections.Generic;
using System.Text;

namespace QueryStringProcessing
{
    public class TestModel : BaseModel
    {
        public string StringProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public List<string> ListStringProperties { get; set; }
        public List<string> People { get; set; }
        public TestModel2 TestModel2 { get; set; }
    }

    public class TestModel2
    {
        public int IntProperty { get; set; }
        public TestModel3 TestModel3 { get; set; }
    }

    public class TestModel3
    {
        public Guid GuidProperty { get; set; }
    }

    public class BaseModel
    {

    }
}
