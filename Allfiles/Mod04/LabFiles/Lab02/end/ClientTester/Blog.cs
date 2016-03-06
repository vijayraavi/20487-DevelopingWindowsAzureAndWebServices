using System;
using System.Collections.Generic;

namespace ClientTester
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}
