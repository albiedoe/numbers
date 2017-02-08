using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Models
{
    public class FixedQueue
    {
        Queue<DayInTheMarket> queue = new Queue<DayInTheMarket>();
        private int Size {get; set;}
        public decimal MovingAverage => queue.Average(day => day.ClosingPrice);
        public decimal Change => queue.First().ClosingPrice / queue.Last().ClosingPrice;
        
        public FixedQueue(int size){
            Size = size;
        }

        public void Add(DayInTheMarket day){
            queue.Enqueue(day);
            if(queue.Count()>Size){
                queue.Dequeue();
            }
        }

        
    }

}