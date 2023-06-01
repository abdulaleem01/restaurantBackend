using System;
using Entities.DbEntities;
namespace Entities
{
    public class DbRepo : IRepo
    {
        RestaurantDbContext context = new RestaurantDbContext();

        public AdminInfo AddAdmin(AdminInfo adminInfo)
        {
            context.AdminInfos.Add(adminInfo);
            context.SaveChanges();
            return adminInfo;
        }

        public DishesInfo AddDish(DishesInfo dishesInfo)
        {
            context.DishesInfos.Add(dishesInfo);
            context.SaveChanges();
            return dishesInfo;
        }

        public DishesInfo ViewDish(int DishId)
        {
            return context.DishesInfos.Where(x => x.DishId == DishId).FirstOrDefault();
        }


        public TableSeating AddTables(TableSeating tableSeating)
        {
            context.TableSeatings.Add(tableSeating);
            context.SaveChanges();
            return tableSeating;
        }

        public int AdminLogin(string Email, string Password)
        {

            try
            {
                AdminInfo q = context.AdminInfos.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
                if (q.Email == Email && q.Password == Password)
                {
                    return q.AdminId;
                }
                else
                {
                    return 0;
                }
            }
            catch (NullReferenceException)
            {
                return 0;
            }


        }

        public DishesInfo DeleteDish(int id)
        {
            DishesInfo q = context.DishesInfos.Where(x => x.DishId == id).FirstOrDefault();
            context.Remove(q);
            context.SaveChanges();
            return q;
        }

        public TableSeating DeleteTable(int id)
        {
            TableSeating q = context.TableSeatings.Where(x => x.TableSeatingId == id).FirstOrDefault();
            context.Remove(q);
            context.SaveChanges();
            return q;
        }

        public DishesInfo UpdateDish(DishesInfo dishesInfo)
        {
            context.DishesInfos.Update(dishesInfo);
            context.SaveChanges();
            return dishesInfo;
        }

        public TableSeating UpdateTable(TableSeating tableSeating)
        {
            context.TableSeatings.Update(tableSeating);
            context.SaveChanges();
            return tableSeating;
        }

        public IEnumerable<DishesInfo> ViewAllDish()
        {
            List<DishesInfo> values = context.DishesInfos.ToList();

            return values;
        }

        public IEnumerable<TableSeating> ViewAllTable()
        {
            List<TableSeating> values = context.TableSeatings.ToList();
            return values;
        }

        public int CustomerLogin(string Email, string Password)
        {
            try
            {
                CustomerDetail customerDetail = context.CustomerDetails.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
                if (customerDetail.Email == Email && customerDetail.Password == Password)
                {
                    return customerDetail.CustomerId;
                }
                else
                {
                    return 0;
                }
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        public CustomerDetail AddCustomer(CustomerDetail customerDetail)
        {
            context.CustomerDetails.Add(customerDetail);
            context.SaveChanges();
            return customerDetail;

        }

        public CustomerDetail GetCustomerDetailById(int id)
        {
            CustomerDetail customerDetail = context.CustomerDetails.Where(x => x.CustomerId == id).FirstOrDefault();
            return customerDetail;
        }

        public CustomerDetail UpdateCustomerDetails(CustomerDetail customerDetail)
        {
            context.CustomerDetails.Update(customerDetail);
            context.SaveChanges();
            return customerDetail;
        }
        //

        //VisitDetails
        public VisitDetail AddVisitDetail(VisitDetail visitDetail)
        {
            context.VisitDetails.Add(visitDetail);
            context.SaveChanges();
            return visitDetail;
        }

        public VisitDetail GetVisitDetailById(int id)
        {
            VisitDetail visit = context.VisitDetails.Where(x => x.VisitId == id).FirstOrDefault();
            return visit;
        }

        public VisitDetail ChangeDeliveryStatus(int id, int status)
        {
            VisitDetail visit = context.VisitDetails.Where(x => x.VisitId == id).FirstOrDefault();
            visit.DeliveryStatus = status;
            context.VisitDetails.Update(visit);
            context.SaveChanges();
            return visit;
        }

        public VisitDetail ChangePaymentStatus(int id, int status)
        {
            VisitDetail visit = context.VisitDetails.Where(x => x.VisitId == id).FirstOrDefault();
            visit.PaymentStatus = status;
            context.VisitDetails.Update(visit);
            context.SaveChanges();
            return visit;
        }
        //

        //Order Details
        public OrderDetail AddOrderDetail(OrderDetail orderDetail)
        {
            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();
            return orderDetail;
        }

        public OrderDetail DeleteOrderDetails(int id)
        {
            OrderDetail order = context.OrderDetails.Where(x => x.OrderId == id).FirstOrDefault();
            context.OrderDetails.Remove(order);
            context.SaveChanges();
            return order;

        }

        public IEnumerable<OrderDetail> GetOrdersByVisitId(int visitId)
        {
            IQueryable<OrderDetail> orderDetails = context.OrderDetails.Where(x => x.VisitId == visitId);
            return orderDetails;
        }

        public int TotalOrderAmount(int visitId)
        {
            int Total = 0;
            IEnumerable<OrderDetail> orders = context.OrderDetails.Where(x => x.VisitId == visitId).ToList();
            List<int> dishIds = new List<int>();
            foreach (OrderDetail x in orders)
            {
                DishesInfo dishes = context.DishesInfos.Where(y => y.DishId == x.DishId).FirstOrDefault();
                Total += dishes.Price;




            }

            return Total;


        }

        public int AddMultipleOrders(IEnumerable<OrderDetail> orderDetails)
        {
            foreach (OrderDetail orderDetail in orderDetails)
            {
                context.OrderDetails.Add(orderDetail);
                context.SaveChanges();
            }
            return 1;
        }

        public IEnumerable<DishesInfo> GetDishInfoByVisitId(int VisitId)
        {
            List<OrderDetail> orderlist = context.OrderDetails.Where(x => x.VisitId == VisitId).ToList();
            List<DishesInfo> dishList = new List<DishesInfo>();
            foreach (OrderDetail orderDetail in orderlist)
            {
                dishList.Add(context.DishesInfos.Where(x => x.DishId == orderDetail.DishId).FirstOrDefault());
            }

            return dishList;


        }

        public IEnumerable<VisitDetail> GetAllVisitDetailsByCustomerId(int id)
        {
            IQueryable<VisitDetail> visitDetails = context.VisitDetails.Where(x => x.CustomerId == id);
            return visitDetails;
        }

        public CustomerDetail ChangePassword(string email)
        {
            CustomerDetail customerDetail = context.CustomerDetails.Where(x => x.Email == email).FirstOrDefault();

            return customerDetail;
        }

        public IEnumerable<VisitDetail> GetVisitDetailsByDeliveryStatus(int status)
        {
            return context.VisitDetails.Where(x => x.DeliveryStatus == status);
        }

        public int CheckVisitStatusChanges()
        {
            return context.VisitDetails.Where(x => x.DeliveryStatus == 1).Count();
        }

        public int DeleteAllVisit(IEnumerable<VisitDetail> visitDetails)
        {
            context.VisitDetails.RemoveRange(visitDetails);
            return context.SaveChanges();
        }

        public IEnumerable<VisitDetail> GetAllVisitDetials()
        {
            return context.VisitDetails.ToList();
        }

    }
}

