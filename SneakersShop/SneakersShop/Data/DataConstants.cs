namespace SneakersShop.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;
        }
        public class Sneakers
        {
            public const int BrandMaxLength = 20;
            public const int BrandMinLength = 1;

            public const int ModelMaxLength = 30;
            public const int ModelMinLength = 1;

            public const int DescriptionMinLength = 10;

            public const int ColorMaxLength = 20;
            public const int ColorMinLength = 1;
        }

        public class Category
        {
            public const int NameMaxLength = 25;
        }

        public class Seller
        {
            public const int NameMaxLength = 25;
            public const int NameMinLength = 2;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }

    }
}
