using System;
using Entities.DbEntities;
using Models;
namespace BuisnessLogic
{
    public class Mapper
    {
        //Model To Entity
        public AdminInfo ModelAdminToDb(AdminModel adminModel)
        {
            AdminInfo adminInfo = new AdminInfo()
            {
                AdminId = adminModel.AdminId,
                Name = adminModel.Name,
                Email = adminModel.Email,
                Password = adminModel.Password
            };

            return adminInfo;
        }

        //Entity To Model
        public AdminModel DbAdminToModel(AdminInfo adminModel)
        {
            AdminModel adminInfo = new AdminModel()
            {
                AdminId = adminModel.AdminId,
                Name = adminModel.Name,
                Email = adminModel.Email,
                Password = adminModel.Password
            };

            return adminInfo;
        }




        //Model To Entity
        public DishesInfo ModelDishToDb(DishesModel dishesModel)
        {
            DishesInfo dishesInfo = new DishesInfo()
            {
                DishId = dishesModel.DishId,
                Name = dishesModel.Name,
                Description = dishesModel.Description,
                Price = dishesModel.Price,
                ImageUrl = dishesModel.ImageUrl,
                CookingTime = dishesModel.CookingTime

            };
            return dishesInfo;
        }

        //Entity To Model
        public DishesModel DbDishToModel(DishesInfo dishesModel)
        {
            DishesModel dishesInfo = new DishesModel()
            {
                DishId = dishesModel.DishId,
                Name = dishesModel.Name,
                Description = dishesModel.Description,
                Price = dishesModel.Price,
                ImageUrl = dishesModel.ImageUrl,
                CookingTime = dishesModel.CookingTime

            };
            return dishesInfo;
        }



        //Model To Entity
        public TableSeating ModelTableSeatingToDb(TableSeatingModel tableSeatingModel)
        {
            TableSeating tableSeating = new TableSeating()
            {
                TableSeatingId = tableSeatingModel.TableSeatingId,
                TableNo = tableSeatingModel.TableNo,
                Description = tableSeatingModel.Description
            };

            return tableSeating;
        }

        //Entity To Model

        public TableSeatingModel DbTableSeatingToModel(TableSeating tableSeatingModel)
        {
            TableSeatingModel tableSeating = new TableSeatingModel()
            {
                TableSeatingId = tableSeatingModel.TableSeatingId,
                TableNo = tableSeatingModel.TableNo,
                Description = tableSeatingModel.Description
            };

            return tableSeating;
        }

        //Model To Entity

        public CustomerDetail ModelCustomerToDb(CustomerModel customerModel)
        {
            CustomerDetail customerDetail = new CustomerDetail()
            {
                CustomerId = customerModel.CustomerId,
                Email = customerModel.Email,
                Name = customerModel.Name,
                PhoneNumber = customerModel.PhoneNumber,
                Password = customerModel.Password
            };

            return customerDetail;
        }

        //Entity To Model
        public CustomerModel DbCustomerToModel(CustomerDetail customerModel)
        {
            CustomerModel customerDetail = new CustomerModel()
            {
                CustomerId = customerModel.CustomerId,
                Email = customerModel.Email,
                Name = customerModel.Name,
                PhoneNumber = customerModel.PhoneNumber,
                Password = customerModel.Password
            };

            return customerDetail;
        }



        //Model To Entity
        public VisitDetail ModelVisitToDb(VisitDetailModel visitDetailModel)
        {
            VisitDetail visitDetail = new VisitDetail()
            {
                VisitId = visitDetailModel.VisitId,
                CustomerId = visitDetailModel.CustomerId,
                TableId = visitDetailModel.TableId,
                Date = visitDetailModel.Date,
                Time = visitDetailModel.Time,
                DeliveryStatus = visitDetailModel.DeliveryStatus,
                PaymentStatus = visitDetailModel.PaymentStatus

            };

            return visitDetail;
        }

        //Entity To Model
        public VisitDetailModel DbVisitToModel(VisitDetail visitDetailModel)
        {
            VisitDetailModel visitDetail = new VisitDetailModel()
            {
                VisitId = visitDetailModel.VisitId,
                CustomerId = visitDetailModel.CustomerId,
                TableId = visitDetailModel.TableId,
                Date = visitDetailModel.Date,
                Time = visitDetailModel.Time,
                DeliveryStatus = visitDetailModel.DeliveryStatus,
                PaymentStatus = visitDetailModel.PaymentStatus

            };

            return visitDetail;
        }

        //Model To Entity
        public OrderDetail ModelOrderToDb(OrderDetailModel orderDetailModel)
        {
            OrderDetail orderDetail = new OrderDetail()
            {
                OrderId = orderDetailModel.OrderId,
                VisitId = orderDetailModel.VisitId,
                DishId = orderDetailModel.DishId
            };

            return orderDetail;
        }

        //Entity To Model
        public OrderDetailModel DbOrderToModel(OrderDetail orderDetailModel)
        {
            OrderDetailModel orderDetail = new OrderDetailModel()
            {
                OrderId = orderDetailModel.OrderId,
                VisitId = orderDetailModel.VisitId,
                DishId = orderDetailModel.DishId
            };

            return orderDetail;
        }


        //Custom Mappers
        public OrderDishModel CombineOrderAndDishes(OrderDetailModel orderDetailModel, DishesModel dishesModel)
        {
            OrderDishModel orderDishModel = new OrderDishModel()
            {
                OrderId = orderDetailModel.OrderId,
                VisitId = orderDetailModel.VisitId,
                DishId = orderDetailModel.VisitId,
                Name = dishesModel.Name,
                Description = dishesModel.Description,
                Price = dishesModel.Price,
                ImageUrl = dishesModel.ImageUrl,
                CookingTime = dishesModel.CookingTime

            };

            return orderDishModel;
        }
    }
}

