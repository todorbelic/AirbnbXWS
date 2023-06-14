using System.ComponentModel;

namespace AccommodationService.Enums
{
    public class HolidaysEnum
    {
        public static  HolidaysEnum CatholicChristmas = new HolidaysEnum(1,"Catholic Christmas",new DateOnly(0000,12,25), new DateOnly(0000, 12, 25));
        public static  HolidaysEnum OrthodoxChristmas = new HolidaysEnum(2,"Orthodox Christmas", new DateOnly(0000, 1, 7), new DateOnly(0000, 1,7));
        public static  HolidaysEnum Easter = new HolidaysEnum(3,"Easter", new DateOnly(0000, 3, 31), new DateOnly(0000, 3, 31));
        public static  HolidaysEnum NewYear = new HolidaysEnum(4,"New Year", new DateOnly(0000, 12, 31), new DateOnly(0000, 1, 1));
        public static  HolidaysEnum LaborDay = new HolidaysEnum(5, "Labor Day", new DateOnly(0000, 5, 1), new DateOnly(0000, 5, 1));


        public int Id { get; }
        public string Name { get; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        private HolidaysEnum(int id,string name, DateOnly startDate, DateOnly endDate)
        {
            Id= id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
        public static HolidaysEnum FindHoliday(int id)
        {
            switch (id)
            {
                case 1: return CatholicChristmas;
                case 2: return OrthodoxChristmas;
                case 3: return Easter;
                case 4: return NewYear;
                case 5: return LaborDay;
                default: return null;

            }
        }
    }    
}

