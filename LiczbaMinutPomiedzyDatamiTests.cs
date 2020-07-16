using EO.Serwis.Portal.DataAccess.Contract.POCO;
using EO.Serwis.Portal.DataAccess.Contract.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EscalationStatelessService.Test
{
    public class LiczbaMinutPomiedzyDatamiTests
    {
        [Fact]
        public void WyliczLiczbeMinutPomiedzyDatamizKalendarza_Test()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(18,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,12,23), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "Boże narodzenie I dzień świąt", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,12,24), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 9 , IdKalendarza =1 , NazwaDnia = "Boże narodzenie II dzień świąt", RodzajDnia = "Wolny" }
            };

            //Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            DateTime dataPoczatkowa = new DateTime(2019, 12, 23); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 12, 28);  //TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 1680;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);
        }

        [Fact]
        public void WyliczLiczbeMinutPomiedzyDatamizKalendarza_Test_1()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            DateTime dataPoczatkowa = new DateTime(2019, 09, 16, 10, 20, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 09, 18, 14, 15, 00);  //TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 1315;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);
        }

        [Fact]
        public void WyliczLiczbeMinutPomiedzyDatamizKalendarza_Test_2()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            DateTime dataPoczatkowa = new DateTime(2019, 08, 01); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 08, 09);  //TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 3780;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);
        }

        [Fact]
        public void WyliczLiczbeMinutPomiedzyDatamizKalendarza_Test_3()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "Narodowe święto Niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            DateTime dataPoczatkowa = new DateTime(2019, 11, 11); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 11, 16);  //TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 2160;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);
        }

        [Fact]
        public void WyliczLiczbeMinutPomiedzyDatamizKalendarza_Test_4()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            DateTime dataPoczatkowa = new DateTime(2019, 09, 01); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 09, 14);  //TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 5400;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);
        }
        [Fact]
        public void WyliczLiczbeMinutPomiedzyDatamizKalendarza_Test_5()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(18,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(10,00,00), GodzinaDo = new TimeSpan(16,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            DateTime dataPoczatkowa = new DateTime(2019, 10, 12); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 10, 16);  //TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 1500;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);
        }

        [Fact]
        public void WyliczLiczbeMinutPomiedzyDatamizKalendarza_Test_6()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(18,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(10,00,00), GodzinaDo = new TimeSpan(16,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            DateTime dataPoczatkowa = new DateTime(2019, 10, 12, 8, 40, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 10, 16, 15, 15, 00);  //TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 1475;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);
        }
        [Fact]
        public void WyliczLiczbeMinutPomiedzyDatamizKalendarza_Test_7()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "Narodowe święto Niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            DateTime dataPoczatkowa = new DateTime(2019, 11, 11, 9, 40, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 11, 15, 16, 10, 00);  //TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 2110;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);
        }


        [Fact]
        public void LicznikProcetowegoWykorzystaniaCzasuSLA_Test_1()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "Narodowe święto Niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            LicznikProcentowegoWykorzystaniaSLA testClass2 = new LicznikProcentowegoWykorzystaniaSLA();

            DateTime dataPoczatkowa = new DateTime(2019, 11, 11, 9, 40, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 11, 15, 16, 10, 00);  //TODO: data testowa
            long idKalendarza = 1;

            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);
            var expectedValue = 2110;
            Assert.Equal(expectedValue, assertValue);

            var czasSLA = 2130;
            var assertValue2 = testClass2.Przelicz(expectedValue, czasSLA);
            var expectedValue2 = 20;
            Assert.Equal(expectedValue2, assertValue2);
        }
        [Fact]
        public void LicznikProcetowegoWykorzystaniaCzasuSLA_Test_2()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(18,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(10,00,00), GodzinaDo = new TimeSpan(16,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            LicznikProcentowegoWykorzystaniaSLA testClass2 = new LicznikProcentowegoWykorzystaniaSLA();

            DateTime dataPoczatkowa = new DateTime(2019, 10, 12, 8, 40, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 10, 16, 15, 15, 00);  //TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 1475;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);

            var czasSLA = 1500;
            var assertValue2 = testClass2.Przelicz(expectedValue, czasSLA);
            var expectedValue2 = 25;
            Assert.Equal(expectedValue2, assertValue2);
        }
        [Fact]
        public void LicznikProcetowegoWykorzystaniaCzasuSLA_Test_3()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 9 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,01), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 10 , IdKalendarza =1 , NazwaDnia = "wszystkich świętych", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 11 , IdKalendarza =1 , NazwaDnia = "święto niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            LicznikProcentowegoWykorzystaniaSLA testClass2 = new LicznikProcentowegoWykorzystaniaSLA();

            DateTime dataPoczatkowa = new DateTime(2019,10,21, 16,19,00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019,10,23, 11,00,00);////TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 761;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);

            var czasSLA = 7200;
            var assertValue2 = testClass2.Przelicz(expectedValue, czasSLA);
            var expectedValue2 = 6439;
            Assert.Equal(expectedValue2, assertValue2);
        }
        [Fact]
        public void LicznikProcetowegoWykorzystaniaCzasuSLA_Test_4()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 9 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,01), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 10 , IdKalendarza =1 , NazwaDnia = "wszystkich świętych", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 11 , IdKalendarza =1 , NazwaDnia = "święto niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            LicznikProcentowegoWykorzystaniaSLA testClass2 = new LicznikProcentowegoWykorzystaniaSLA();

            DateTime dataPoczatkowa = new DateTime(2019, 10, 22, 04, 41, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 10, 23, 11, 28, 00);////TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 748;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);

            var czasSLA = 4320;
            var assertValue2 = testClass2.Przelicz(expectedValue, czasSLA);
            var expectedValue2 = 3572;
            Assert.Equal(expectedValue2, assertValue2);
        }
        [Fact]
        public void LicznikProcetowegoWykorzystaniaCzasuSLA_Test_5()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 9 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,01), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 10 , IdKalendarza =1 , NazwaDnia = "wszystkich świętych", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 11 , IdKalendarza =1 , NazwaDnia = "święto niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            LicznikProcentowegoWykorzystaniaSLA testClass2 = new LicznikProcentowegoWykorzystaniaSLA();

            DateTime dataPoczatkowa = new DateTime(2019, 10, 16, 15, 33, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 10, 21, 12, 33, 00);////TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 1440;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);

            var czasSLA = 1440;
            var assertValue2 = testClass2.Przelicz(expectedValue, czasSLA);
            var expectedValue2 = 0;
            Assert.Equal(expectedValue2, assertValue2);
        }
        [Fact]
        public void LicznikProcetowegoWykorzystaniaCzasuSLA_Test_6()//id = 236108
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 9 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,01), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 10 , IdKalendarza =1 , NazwaDnia = "wszystkich świętych", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 11 , IdKalendarza =1 , NazwaDnia = "święto niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            LicznikProcentowegoWykorzystaniaSLA testClass2 = new LicznikProcentowegoWykorzystaniaSLA();

            DateTime dataPoczatkowa = new DateTime(2019, 10, 09, 11, 21, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 10, 23, 12, 37, 00);////TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 5476;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);

            var czasSLA = 4320;
            var assertValue2 = testClass2.Przelicz(expectedValue, czasSLA);
            var expectedValue2 = -1156;
            Assert.Equal(expectedValue2, assertValue2);
        }
        [Fact]
        public void LicznikProcetowegoWykorzystaniaCzasuSLA_Test_7()//id = 236076
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 9 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,01), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 10 , IdKalendarza =1 , NazwaDnia = "wszystkich świętych", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 11 , IdKalendarza =1 , NazwaDnia = "święto niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            LicznikProcentowegoWykorzystaniaSLA testClass2 = new LicznikProcentowegoWykorzystaniaSLA();

            DateTime dataPoczatkowa = new DateTime(2019, 09, 30, 16, 04, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 10, 23, 12, 37, 00);////TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 8973;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);

            var czasSLA = 480;
            var assertValue2 = testClass2.Przelicz(expectedValue, czasSLA);
            var expectedValue2 = -8493;
            Assert.Equal(expectedValue2, assertValue2);
        }
        [Fact]
        public void LicznikProcetowegoWykorzystaniaCzasuSLA_Test_8()//id = 236077
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 9 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,01), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 10 , IdKalendarza =1 , NazwaDnia = "wszystkich świętych", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 11 , IdKalendarza =1 , NazwaDnia = "święto niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            LicznikProcentowegoWykorzystaniaSLA testClass2 = new LicznikProcentowegoWykorzystaniaSLA();

            DateTime dataPoczatkowa = new DateTime(2019, 10, 04, 23, 53, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 10, 23, 12, 37, 00);////TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 6757;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);

            var czasSLA = 480;
            var assertValue2 = testClass2.Przelicz(expectedValue, czasSLA);
            var expectedValue2 = -6277;
            Assert.Equal(expectedValue2, assertValue2);
        }
        [Fact]
        public void LicznikProcetowegoWykorzystaniaCzasuSLA_Test_9()
        {
            IEnumerable<KalendarzeDniPOCO> kalendarz = new List<KalendarzeDniPOCO>
            {   new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 3 , IdKalendarza =1 , NazwaDnia = "1", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 4 , IdKalendarza =1 , NazwaDnia = "2", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 5 , IdKalendarza =1 , NazwaDnia = "3", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 6 , IdKalendarza =1 , NazwaDnia = "4", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(08,00,00), GodzinaDo = new TimeSpan(17,00,00), IdDniaWKalendarzu = 7 , IdKalendarza =1 , NazwaDnia = "5", RodzajDnia = "Roboczy" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 8 , IdKalendarza =1 , NazwaDnia = "6", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = null, GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 9 , IdKalendarza =1 , NazwaDnia = "7", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,01), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 10 , IdKalendarza =1 , NazwaDnia = "wszystkich świętych", RodzajDnia = "Wolny" }
                ,new KalendarzeDniPOCO(){Data = new DateTime(2019,11,11), GodzinaOd = new TimeSpan(00,00,00), GodzinaDo = new TimeSpan(00,00,00), IdDniaWKalendarzu = 11 , IdKalendarza =1 , NazwaDnia = "święto niepodległości", RodzajDnia = "Wolny" }
            };

            // Mock the Products Repository using Moq
            Mock<IKalendarzeDniRepository> mockProductRepository = new Mock<IKalendarzeDniRepository>();
            mockProductRepository.Setup(p => p.Select(It.IsAny<System.Linq.Expressions.Expression<Func<KalendarzeDniPOCO, bool>>>())).Returns(kalendarz);

            LiczbaMinutPomiedzyDatami testClass = new LiczbaMinutPomiedzyDatami();
            LicznikProcentowegoWykorzystaniaSLA testClass2 = new LicznikProcentowegoWykorzystaniaSLA();

            DateTime dataPoczatkowa = new DateTime(2019, 10, 31, 23, 53, 00); //TODO: data testowa
            DateTime dataKoncowa = new DateTime(2019, 11, 12, 12, 37, 00);////TODO: data testowa
            long idKalendarza = 1;
            var expectedValue = 2977;
            var assertValue = testClass.WyliczLiczbeMinutPomiedzyDatamizKalendarza(dataPoczatkowa, dataKoncowa, idKalendarza, mockProductRepository.Object);

            Assert.Equal(expectedValue, assertValue);

            var czasSLA = 3000;
            var assertValue2 = testClass2.Przelicz(expectedValue, czasSLA);
            var expectedValue2 = 23;
            Assert.Equal(expectedValue2, assertValue2);
        }
    }
}
