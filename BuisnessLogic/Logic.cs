using System;
using Models;
using Entities.DbEntities;
using Entities;

namespace BuisnessLogic
{
    public class Logic : ILogic
    {
        DbRepo repo = new DbRepo();
        Mapper Mapper = new Mapper();

        public AdminModel AddAdmin(AdminModel adminInfo)
        {
            return Mapper.DbAdminToModel(repo.AddAdmin(Mapper.ModelAdminToDb(adminInfo)));

        }

        public DishesModel AddDish(DishesModel dishesInfo)
        {
            return Mapper.DbDishToModel(repo.AddDish(Mapper.ModelDishToDb(dishesInfo)));

        }

        public DishesModel ViewDish(int DishId)
        {
            return Mapper.DbDishToModel(repo.ViewDish(DishId));

        }


        public TableSeatingModel AddTables(TableSeatingModel tableSeating)
        {
            return Mapper.DbTableSeatingToModel(repo.AddTables(Mapper.ModelTableSeatingToDb(tableSeating)));

        }

        public int AdminLogin(string Email, string Password)
        {
            return repo.AdminLogin(Email, Password);
        }

        public DishesModel DeleteDish(int id)
        {
            return Mapper.DbDishToModel(repo.DeleteDish(id));

        }

        public TableSeatingModel DeleteTable(int id)
        {
            return Mapper.DbTableSeatingToModel(repo.DeleteTable(id));

        }

        public DishesModel UpdateDish(DishesModel dishesInfo)
        {
            return Mapper.DbDishToModel(repo.UpdateDish(Mapper.ModelDishToDb(dishesInfo)));

        }

        public TableSeatingModel UpdateTable(TableSeatingModel tableSeating)
        {
            return Mapper.DbTableSeatingToModel(repo.UpdateTable(Mapper.ModelTableSeatingToDb(tableSeating)));
        }

        public IEnumerable<DishesModel> ViewAllDish()
        {
            List<DishesModel> dishes = new List<DishesModel>();
            foreach (var x in repo.ViewAllDish())
            {
                dishes.Add(Mapper.DbDishToModel(x));
            }

            return dishes;
        }

        public IEnumerable<TableSeatingModel> ViewAllTable()
        {
            List<TableSeatingModel> tableSeatingModels = new List<TableSeatingModel>();
            foreach (var x in repo.ViewAllTable())
            {
                tableSeatingModels.Add(Mapper.DbTableSeatingToModel(x));
            }
            return tableSeatingModels;
        }

        public int CustomerLogin(string Email, string Password)
        {

            return repo.CustomerLogin(Email, Password);
        }
        public CustomerModel AddCustomer(CustomerModel customerDetail)
        {
            return Mapper.DbCustomerToModel(repo.AddCustomer(Mapper.ModelCustomerToDb(customerDetail)));
        }
        public CustomerModel GetCustomerDetailById(int id)
        {
            return Mapper.DbCustomerToModel(repo.GetCustomerDetailById(id));
        }
        public CustomerModel UpdateCustomerDetails(CustomerModel customerDetail)
        {
            return Mapper.DbCustomerToModel(repo.UpdateCustomerDetails(Mapper.ModelCustomerToDb(customerDetail)));
        }
        //

        //VisitDetails
        public VisitDetailModel AddVisitDetail(VisitDetailModel visitDetail)
        {
            return Mapper.DbVisitToModel(repo.AddVisitDetail(Mapper.ModelVisitToDb(visitDetail)));
        }
        public VisitDetailModel GetVisitDetailById(int id)
        {
            return Mapper.DbVisitToModel(repo.GetVisitDetailById(id));
        }
        public VisitDetailModel ChangeDeliveryStatus(int id, int status)
        {
            return Mapper.DbVisitToModel(repo.ChangeDeliveryStatus(id, status));
        }
        public VisitDetailModel ChangePaymentStatus(int id, int status)
        {
            return Mapper.DbVisitToModel(repo.ChangePaymentStatus(id, status));

        }
        //

        //Order Details
        public OrderDetailModel AddOrderDetail(OrderDetailModel orderDetail)
        {
            return Mapper.DbOrderToModel(repo.AddOrderDetail(Mapper.ModelOrderToDb(orderDetail)));
        }
        public OrderDetailModel DeleteOrderDetails(int id)
        {
            return Mapper.DbOrderToModel(repo.DeleteOrderDetails(id));
        }
        public IEnumerable<OrderDetailModel> GetOrdersByVisitId(int visitId)
        {
            List<OrderDetailModel> orders = new List<OrderDetailModel>();
            foreach (var x in repo.GetOrdersByVisitId(visitId))
            {
                orders.Add(Mapper.DbOrderToModel(x));

            }
            return orders;
        }
        public int TotalOrderAmount(int visitId)
        {
            return repo.TotalOrderAmount(visitId);
        }

        public int AddMultipleOrders(IEnumerable<OrderDetailModel> orderDetails)
        {
            List<OrderDetail> orderDetails1 = new List<OrderDetail>();
            foreach (OrderDetailModel orderDetail in orderDetails)
            {
                orderDetails1.Add(Mapper.ModelOrderToDb(orderDetail));
            }

            return repo.AddMultipleOrders(orderDetails1);
        }

        public IEnumerable<DishesModel> GetDishInfoByVisitId(int VisitId)
        {
            List<DishesModel> dishesModels = new List<DishesModel>();
            foreach (DishesInfo dishesInfo in repo.GetDishInfoByVisitId(VisitId))
            {
                dishesModels.Add(Mapper.DbDishToModel(dishesInfo));
            }

            return dishesModels;
        }

        public IEnumerable<OrderDishModel> GetOrderDishByVisitId(int VisitId)
        {
            //IEnumerable<DishesInfo> dishesInfos = repo.GetDishInfoByVisitId(VisitId);
            //IEnumerable<OrderDetail> orderDetails = repo.GetOrdersByVisitId(VisitId);
            List<OrderDishModel> orderDishModels = new List<OrderDishModel>();
            List<OrderDetail> orderss = repo.GetOrdersByVisitId(VisitId).ToList();
            foreach (OrderDetail orderDetail in orderss)
            {
                //repo.ViewDish(orderDetail.DishId)
                orderDishModels.Add(Mapper.CombineOrderAndDishes(Mapper.DbOrderToModel(orderDetail), Mapper.DbDishToModel(repo.ViewDish(orderDetail.DishId))));

            }

            return orderDishModels;

        }

        public IEnumerable<VisitDetailModel> GetAllVisitDetailsByCustomerId(int id)
        {
            List<VisitDetailModel> visitDetails = new List<VisitDetailModel>();
            foreach (VisitDetail visitDetail in repo.GetAllVisitDetailsByCustomerId(id))
            {
                visitDetails.Add(Mapper.DbVisitToModel(visitDetail));
            }
            return visitDetails;
        }

        public int ChangePassword(string email, string oldpass, string newpass)
        {
            CustomerDetail customer = repo.ChangePassword(email);
            if (customer.Email == email && customer.Password == oldpass)
            {
                customer.Password = newpass;
                repo.UpdateCustomerDetails(customer);

                return 1;
            }
            else
            {
                return 0;
            }
        }



    }
}

