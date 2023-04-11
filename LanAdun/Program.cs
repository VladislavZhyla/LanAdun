using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LanAdun
{
   
    public abstract class TrainHandler
    {
        protected TrainHandler successor;

    public void SetSuccessor(TrainHandler successor)
        {
            this.successor = successor;
        }

        public abstract string Handle(int numberOfCars, string carType);
    }

    public class FreightTrainHandler : TrainHandler
    {
        public override string Handle(int numberOfCars, string carType)
        {
            if (carType == "вантажний")
            {
                return $"Створено {numberOfCars} вантажних вагонів.";
            }
            else if (successor != null)
            {
                return successor.Handle(numberOfCars, carType);
            }
            else
            {
                return "Не вдалося створити запитаний тип вагона";
            }
        }
    }

    public class PassengerTrainHandler : TrainHandler
    {
        public override string Handle(int numberOfCars, string carType)
        {
            if (carType == "пасажирський")
            {
                return $"Створено {numberOfCars} пасажирських вагонів.";
            }
            else if (successor != null)
            {
                return successor.Handle(numberOfCars, carType);
            }
            else
            {
                return "Не вдалося створити запитаний тип вагона.";
            }
        }
    }
    public class DiningCarHandler : TrainHandler
    {
        public override string Handle(int numberOfCars, string carType)
        {
            if (carType == "ресторанний")
            {
                return $"Створено {numberOfCars} ресторанних вагонів.";
            }
            else if (successor != null)
            {
                return successor.Handle(numberOfCars, carType);
            }
            else
            {
                return "Не вдалося створити запитаний тип вагона.";
            }
        }
    }
    public class TrainVisualization
    {
        private TrainHandler chain;  
    public TrainVisualization()
        {          
            chain = new FreightTrainHandler();
            TrainHandler passengerHandler = new PassengerTrainHandler();
            TrainHandler diningHandler = new DiningCarHandler();
            chain.SetSuccessor(passengerHandler);
            passengerHandler.SetSuccessor(diningHandler);
        }

        public string CreateTrain(int numberOfCars, string carType)
        {          
            return chain.Handle(numberOfCars, carType);
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            TrainVisualization visualization = new TrainVisualization();
            Console.WriteLine(visualization.CreateTrain(10, "вантажний"));
            Console.WriteLine(visualization.CreateTrain(5, "пасажирський"));
            Console.WriteLine(visualization.CreateTrain(2, "ресторанний"));
            Console.WriteLine(visualization.CreateTrain(3, "гальмовиЙ"));
            Console.ReadKey();
        }
    }
   
    public class TrainVisualizationTest
    {
        public void CreateTrain_Freight_Success()
        {
            TrainVisualization visualization = new TrainVisualization();
            int numberOfCars = 10;
            string carType = "freight";

            string result = visualization.CreateTrain(numberOfCars, carType);

            string expected = $"Created {numberOfCars} freight cars.";
            Assert.Equal(expected, result);
        }

        public void CreateTrain_Passenger_Success()
        {
            TrainVisualization visualization = new TrainVisualization();
            int numberOfCars = 5;
            string carType = "passenger";

            string result = visualization.CreateTrain(numberOfCars, carType);

            string expected = $"Created {numberOfCars} passenger cars.";
            Assert.Equal(expected, result);
        }
      
        public void CreateTrain_Dining_Success()
        {

            TrainVisualization visualization = new TrainVisualization();
            int numberOfCars = 2;
            string carType = "dining";
            string result = visualization.CreateTrain(numberOfCars, carType);
            string expected = $"Created {numberOfCars} dining cars.";
            Assert.Equal(expected, result);
        }
  
        public void CreateTrain_Caboose_Failure()
        {
            TrainVisualization visualization = new TrainVisualization();
            int numberOfCars = 3;
            string carType = "caboose";
            string result = visualization.CreateTrain(numberOfCars, carType);        
            string expected = "Could not create the requested type of car.";
            Assert.Equal(expected, result);
        }
    }

}
