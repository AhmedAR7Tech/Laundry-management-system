namespace Models
{
    public class ClsModelplus
    {
        public class PersonInfoForSign
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Address { get; set; }

            public string PhoneNumber { get; set; }

            public string Password { get; set; }

            public string Role { get; set; }
        }


        public class PersonInfoLogin
        {
            public string PhoneNumber { get; set; }

            public string Password { get; set; }
        }

        public class PersonInfo
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Address { get; set; }

            public string PhoneNumber { get; set; }

            public string Role { get; set; }
        }

        public class ServicesItems
        {
            public int ServicesID { get; set; }

            public string ServicesName { get; set; }

            public int UnitPrice { get; set; }
        }

        public class AddServices
        {
            public string ServicesName { get; set; }

            public int UnitPrice { get; set; }
        }

        public class OrderRequestModel
        {
            public int PersonId { get; set; }
            public string PhoneNumberPlus { get; set; }
            public string AddressPlus { get; set; }
            public decimal TotalAmount { get; set; }

            public List<OrderServicesModel> Services { get; set; }
        }

        public class OrderServicesModel
        {
            public int ServicesID { get; set; }
            public int Quantity { get; set; }

            public int UnitPrice { get; set; }
        }

        public class OrderDetailsWithCustomerModel
        {
            public int OrderID { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime DeliverDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string OrderStatus { get; set; }

            public int PersonID { get; set; }
            public string PersonName { get; set; }
            public string PhoneNumberPlus { get; set; }
            public string AddressPlus { get; set; }

            public List<ServiceOrderItemModel> Services { get; set; }
        }

        public class ServiceOrderItemModel
        {
            public string ServiceName { get; set; }
            public int UnitPrice { get; set; }
            public int Quantity { get; set; }
            public int SubTotal { get; set; }
        }
        public class StatusUpdateModel
        {
            public int OrderID { set; get; }

            public string Status { get; set; }

            public string RegectionReason { get; set; }
        }

        public class GetOrderBYID
        {
            public string Name { get; set; }

            public int OrderID { set; get; }

            public int TotalAmount { get; set; }

            public string Status { get; set; }

            public string refusedReason { get; set; }

            public DateTime OrderDate { get; set; }

            public DateTime DeliveryDate { get; set; }

            public List<ServiceOrderItemModel> ServicesPlus { get; set; }
        }
    }
}
