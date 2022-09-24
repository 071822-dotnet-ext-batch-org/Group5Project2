using BusinessLayer;
using Models;
using Moq;
using RepoLayer;


namespace Tests.StoreFrontAPI
{
    public class BusClassTests
    {


        [Fact]
        public void GetsMyOrdersReturnedOrder()
        {
            //Arrange
            
            Order order = new Order()
            {
                OrderID = Guid.NewGuid(),
                OrderTotal = 500,
                DateOrdered = new DateTime(),
                DateDelivered = new DateTime(),
                Cancelled = false,
                Refunded = false,
                FK_UserID = "d44d63fc-ffa8-4eb7-b81d-644547136d30",
            };

            List<Order?> orderList2 = new List<Order?>();

            orderList2.Add(order);

            var dataSource = new Mock<IRepo>();
            dataSource
                .Setup(m => m.GetMyOrdersAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(orderList2));

            var TheClassBeingTested = new Bus(dataSource.Object);


            //Act

            var TheOrderWasSent = TheClassBeingTested.GetMyOrdersAsync("d44d63fc-ffa8-4eb7-b81d-644547136d30");

            
            //Assert

            Assert.Equal("d44d63fc-ffa8-4eb7-b81d-644547136d30", order.FK_UserID);

        }




    }
}
