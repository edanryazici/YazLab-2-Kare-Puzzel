using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KarePuzzel
{
    internal class Node
    {
        public Point data;
        public Node next;

        
        

        public void Add(Node root, Point data)
        {
            if (root.next == null)
            {
                root.next = new Node();
                root.next.data = data;
                root.next.next = null;
            }
            else
            {
                Node iter;
                iter = root;
                while (iter.next != null)
                {
                    iter = iter.next;
                }
                iter.next = new Node();
                iter.next.data = data;
                iter.next.next = null;
            }
        }

        public List<Point> listele(Node root)
        {
            List<Point> veriler = new List<Point>();
            Node iter;
            iter = root;
            while (iter.next != null)
            {
                  veriler.Add( iter.next.data);
                iter = iter.next;

            }
            return veriler;
        }

         
        public int find_index(Node root, Point b)
        {
            int index_sayacı=0;
           Node iter = root;
          
            while(iter.next.data!=b)
            {
                index_sayacı++;
                iter = iter.next;
            }
            return index_sayacı-1;
        }

        public Point find_data_by_index(Node root,int index)
        {
            Node iter = root;
            for(int i=0;i<=index;i++)
            {
                iter = iter.next;
            }
            return iter.data;

        }
        public Node Clear(Node root)
        {
            root.next = null;
            return root;
        }
        public Node remove(Node root, Point loc)
        {
            Node iter = root;
           while(iter.next.data!=loc)
            {
                iter = iter.next;
            }
            iter.data = iter.next.data;
            return root;
        }

      
        public void replace(Node root,Point b1,Point b2)
        {  
            Node iter1 = root;
            Node iter2 = root;
            //  iter1 = null;
            // iter2 = null;
            while (iter1.next.data != b1)
            {
              
                iter1 = iter1.next;
            }
           
            
            Point data1 = iter1.data;

            while (iter2.next.data != b2)
            {

                iter2 = iter2.next;
            }
            Point data2 = iter2.data;
            iter2.data = data1;
            iter1.data = data2;
           
        }
       
    }
}
