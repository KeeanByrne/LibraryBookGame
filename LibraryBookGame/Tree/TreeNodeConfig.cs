using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBookGame.Tree
{
    //Chat-GPT helped me here 
    class TreeNodeConfig<String>
    {
        public string Data { get; set; }

        public TreeNodeConfig<String> Parent { get; set; }

        public List<TreeNodeConfig<String>> Children { get; set; }

        public int GetHeight()
        {
            int height = 1;
            TreeNodeConfig<String> current = this;
            while (current.Parent != null)
            {
                height++;
                current = current.Parent;

            }

            return height;
        }

    }
}
