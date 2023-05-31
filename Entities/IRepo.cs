using System;
using Entities.DbEntities;
namespace Entities
{
    public interface IRepo
    {
        //Admin Information
        public AdminInfo AddAdmin(AdminInfo adminInfo);
        public int AdminLogin(string Email, string Password);
        //

        //Table Seating
        public TableSeating AddTables(TableSeating tableSeating);
        public IEnumerable<TableSeating> ViewAllTable();
        public TableSeating DeleteTable(int id);
        public TableSeating UpdateTable(TableSeating tableSeating);
        //

        //Dishes Info
        public DishesInfo AddDish(DishesInfo dishesInfo);
        public IEnumerable<DishesInfo> ViewAllDish();
        public DishesInfo DeleteDish(int id);
        public DishesInfo UpdateDish(DishesInfo dishesInfo);
        public DishesInfo ViewDish(int DishId);
        //

        //Customer Details
        public int CustomerLogin(string Email, string Password);
        public CustomerDetail AddCustomer(CustomerDetail customerDetail);
        public CustomerDetail GetCustomerDetailById(int id);
        public CustomerDetail UpdateCustomerDetails(CustomerDetail customerDetail);
        //

        //VisitDetails
        public VisitDetail AddVisitDetail(VisitDetail visitDetail);
        public VisitDetail GetVisitDetailById(int id);
        public VisitDetail ChangeDeliveryStatus(int id, int status);
        public VisitDetail ChangePaymentStatus(int id, int status);
        //

        //Order Details
        public OrderDetail AddOrderDetail(OrderDetail orderDetail);
        public OrderDetail DeleteOrderDetails(int id);
        public IEnumerable<OrderDetail> GetOrdersByVisitId(int visitId);
        public int TotalOrderAmount(int visitId);
        public int AddMultipleOrders(IEnumerable<OrderDetail> orderDetails);


        //

        //Custom
        public IEnumerable<DishesInfo> GetDishInfoByVisitId(int VisitId);
        public IEnumerable<VisitDetail> GetAllVisitDetailsByCustomerId(int id);
        public CustomerDetail ChangePassword(string email);
        public IEnumerable<VisitDetail> GetVisitDetailsByDeliveryStatus(int status);

        public int CheckVisitStatusChanges();






    }
}

