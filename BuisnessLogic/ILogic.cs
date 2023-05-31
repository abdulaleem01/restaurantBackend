using System;
using Entities.DbEntities;
using Models;
namespace BuisnessLogic
{
    public interface ILogic
    {
        //Admin Information
        public AdminModel AddAdmin(AdminModel adminInfo);
        public int AdminLogin(string Email, string Password);
        //

        //Table Seating
        public TableSeatingModel AddTables(TableSeatingModel tableSeating);
        public IEnumerable<TableSeatingModel> ViewAllTable();
        public TableSeatingModel DeleteTable(int id);
        public TableSeatingModel UpdateTable(TableSeatingModel tableSeating);
        //

        //Dishes Info
        public DishesModel AddDish(DishesModel dishesInfo);
        public IEnumerable<DishesModel> ViewAllDish();
        public DishesModel DeleteDish(int id);
        public DishesModel UpdateDish(DishesModel dishesInfo);
        //

        //Customer Details
        public int CustomerLogin(string Email, string Password);
        public CustomerModel AddCustomer(CustomerModel customerDetail);
        public CustomerModel GetCustomerDetailById(int id);
        public CustomerModel UpdateCustomerDetails(CustomerModel customerDetail);
        //

        //VisitDetails
        public VisitDetailModel AddVisitDetail(VisitDetailModel visitDetail);
        public VisitDetailModel GetVisitDetailById(int id);
        public VisitDetailModel ChangeDeliveryStatus(int id, int status);
        public VisitDetailModel ChangePaymentStatus(int id, int status);
        //

        //Order Details
        public OrderDetailModel AddOrderDetail(OrderDetailModel orderDetail);
        public OrderDetailModel DeleteOrderDetails(int id);
        public IEnumerable<OrderDetailModel> GetOrdersByVisitId(int visitId);
        public int TotalOrderAmount(int visitId);
        public int AddMultipleOrders(IEnumerable<OrderDetailModel> orderDetails);

        //Custom
        public IEnumerable<DishesModel> GetDishInfoByVisitId(int VisitId);
        public IEnumerable<OrderDishModel> GetOrderDishByVisitId(int VisitId);
        public IEnumerable<VisitDetailModel> GetAllVisitDetailsByCustomerId(int id);

        public int ChangePassword(string email, string oldpass, string newpass);
        public IEnumerable<DeliveriesModel> GetVisitDetailandOrderDetailsByDeliveryStatus(int status);
        public int CheckVisitStatusChanges();

    }
}

