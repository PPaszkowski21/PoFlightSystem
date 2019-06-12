using System;
using System.Device.Location;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemRezerwacjiBiletówFirmyLotniczej;

namespace TestJednostkowyProjekt
{
    [TestClass]
    public class AddAirport
    {
        [TestMethod]
        public void TestId()
            //Testowanie jednostkowe tworzenia nowego Lotniska
        {
            //testowanie id
            FlightSystem testflightsystem = new FlightSystem();
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test", 1, 2));
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test1", 12, 24));
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test2", 13, 25));
            var listids = testflightsystem.Airports.Select(x => x.Id).ToList();
            int t = listids[1];
            Assert.AreEqual(t, 1);
        }
        [TestMethod]
        public void TestName()
        {
            //testowanie nazwy
            FlightSystem testflightsystem = new FlightSystem();
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test", 1, 2));
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test1", 12, 24));
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test2", 13, 25));
            var listids = testflightsystem.Airports.Select(x => x.Name).ToList();
            string t = listids[1];
            Assert.AreEqual(t, "test1");
        }
        [TestMethod]
        public void TestCor()
        {
            //testowanie szerkosci
            FlightSystem testflightsystem = new FlightSystem();
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test", 1, 2));
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test1", 12, 24));
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test2", 13, 25));
            var listids = testflightsystem.Airports.Select(x => x.Coordinate).ToList();
            GeoCoordinate t = new GeoCoordinate();
            t.Latitude = listids[1].Latitude;
            Assert.AreEqual(t.Latitude,12);
        }
        [TestMethod]
        public void TestCor2()
        {
            //testowanie dlugosci
            FlightSystem testflightsystem = new FlightSystem();
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test", 1, 2));
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test1", 12, 24));
            Helper.SetIDAndAddToList(testflightsystem.Airports, new Airport("test2", 13, 25));
            var listids = testflightsystem.Airports.Select(x => x.Coordinate).ToList();
            GeoCoordinate t = new GeoCoordinate();
            t.Longitude = listids[1].Longitude;
            Assert.AreEqual(t.Longitude,24);
        }
    }
}
