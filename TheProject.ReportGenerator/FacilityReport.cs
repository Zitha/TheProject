using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheProject.Model;

namespace TheProject.ReportGenerator
{
    public class FacilityReport
    {
        // string _currpath = @"C:\Projects\TheProject\TheProject.Web\TheProject.ReportGenerator\Reports\";

        public string GenerateInvoice(Facility facility)
        {
            string _currpath = @"C:\Projects\TheProject\TheProject.Web\TheProject.ReportGenerator\Reports\";
            var doc = new Document(PageSize.A4);
            _currpath = string.Format("{0}{1}.pdf", _currpath, facility.ClientCode);
            var output = new FileStream(_currpath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            doc.AddTitle("Facility Report");

            doc.Open();

            var mainTable = new PdfPTable(2);
            mainTable.DefaultCell.Border = 2;
            mainTable.WidthPercentage = 80;
            mainTable.HorizontalAlignment = Element.ALIGN_CENTER;

            //First Table
            PdfPTable firstTable = GetFirstTable(facility);
            firstTable.SpacingBefore = 10f;
            firstTable.SpacingAfter = 10f;
            //-------------------------//--------------------------------------------------//
            //Second Table
            PdfPTable secondTable = GetSecondTable(facility);
            secondTable.SpacingBefore = 10f;
            secondTable.SpacingAfter = 10f;

            //-------------------------//--------------------------------------------------//
            PdfPTable thirdTable = GetThirdTable(facility);
            thirdTable.SpacingBefore = 10f;
            thirdTable.SpacingAfter = 10f;

            //------------------------//--------------------------------------------------//
            PdfPTable fourthTable = GetFourthTable(facility);
            //-----------------------//---------------------------------------------------//
            doc.Add(mainTable);
            doc.Add(firstTable);
            doc.Add(secondTable);
            doc.Add(thirdTable);
            doc.Add(fourthTable);

            doc.Close();
            output.Close();

            return _currpath;
        }

        private PdfPTable GetFirstTable(Facility facility)
        {
            Image image = Image.GetInstance(ConfigurationManager.AppSettings["LogoPath"]);

            image.ScaleToFit(90f, 70f);
            image.Alignment = 50;
            PdfPTable table = new PdfPTable(4)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                WidthPercentage = 90
            };

            //Labels For PI number and Data
            var imageCell = new PdfPCell
            {
                Colspan = 2,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderColor = BaseColor.DARK_GRAY
            };

            imageCell.AddElement(image);
            imageCell.VerticalAlignment = Element.ALIGN_CENTER;
            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            var invNumberCellData = new PdfPCell
            {
                Colspan = 2,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderColor = BaseColor.BLACK
            };
            invNumberCellData.AddElement(new Phrase(string.Format("User Department:\n Facility Management and Real Estate"),
                FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)));
            invNumberCellData.VerticalAlignment = Element.ALIGN_LEFT;

            table.AddCell(imageCell);
            table.AddCell(invNumberCellData);

            //Labels For project name and Data
            PdfPCell projectNameCell = GetCell("Project Name", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell UCRCellData = GetCell("Ekuruleni Project", BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(projectNameCell);
            table.AddCell(UCRCellData);

            //Labels for Project number and Data
            PdfPCell projectNumberCell = GetCell("Project Number", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell projectNumberCellData = GetCell("Ekuruleni Project", BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(projectNumberCell);
            table.AddCell(projectNumberCellData);

            //Labels for Venus number and Data
            PdfPCell venusNumberCell = GetCell("Venus Number", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell venusNumberCellData = GetCell(facility.ClientCode, BaseColor.BLACK, BaseColor.WHITE);
            venusNumberCellData.Colspan = 3;

            table.AddCell(venusNumberCell);
            table.AddCell(venusNumberCellData);

            //Label for Access Card
            var accessCardCell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_MIDDLE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderColor = BaseColor.DARK_GRAY,
                Colspan = 4,
                PaddingLeft = 170
            };
            accessCardCell.AddElement(new Phrase("Access Card",
                FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)));
            table.AddCell(accessCardCell);

            return table;
        }

        private PdfPTable GetSecondTable(Facility facility)
        {
            //Second Table
            PdfPTable table = new PdfPTable(4)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                WidthPercentage = 90
            };

            //Access Identification Label
            var accessIdentificationCellLbl = new PdfPCell
            {
                Colspan = 5,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderColor = BaseColor.DARK_GRAY,
                BorderColorBottom = BaseColor.BLACK,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingLeft = 170
            };
            accessIdentificationCellLbl.AddElement(new Phrase("Access Identification",
                FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)));
            accessIdentificationCellLbl.VerticalAlignment = Element.ALIGN_LEFT;
            table.AddCell(accessIdentificationCellLbl);

            //ERF NO Label and Data

            PdfPCell erfNoCell = GetCell("ERF NO:", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell erfNoCellData = GetCell(facility.DeedsInfo.ErFNumber, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(erfNoCell);
            table.AddCell(erfNoCellData);
            //Location/Address Label and Data

            PdfPCell locationCell = GetCell("Location/Address", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell locationCellData = GetCell(string.Format("{0} \n {1}", facility.Location.Suburb, facility.Location.StreetAddress
                ), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(locationCell);
            table.AddCell(locationCellData);

            //Region Label and Data
            PdfPCell regionLabel = GetCell("Region", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell regionData = GetCell(facility.Location.Region, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(regionLabel);
            table.AddCell(regionData);

            //Region Label and Data2
            PdfPCell regionLabel2 = GetCell("", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell regionData2 = GetCell("", BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(regionLabel2);
            table.AddCell(regionData2);

            //Province Label and Data
            PdfPCell provinceLabel = GetCell("Province", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell provinceData = GetCell(facility.Location.Province, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(provinceLabel);
            table.AddCell(provinceData);

            //Provice Label and Data2
            PdfPCell provinceLabel2 = GetCell("", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell provinceData2 = GetCell("", BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(provinceLabel2);
            table.AddCell(provinceData2);

            //Local Municipality Label and Data
            PdfPCell municipalityLabel = GetCell("Local Municipality", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell municipalityData = GetCell(facility.Location.LocalMunicipality, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(municipalityLabel);
            table.AddCell(municipalityData);

            //Ward Label and Data2
            PdfPCell wardLabel = GetCell("Ward", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell wardData = GetCell(facility.Location.Region, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(wardLabel);
            table.AddCell(wardData);

            //Local Municipality Label and Data
            PdfPCell titleDeedLabel = GetCell("Title Deed No.", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell titleDeedData = GetCell(facility.DeedsInfo.TitleDeedNumber, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(titleDeedLabel);
            table.AddCell(titleDeedData);

            //Ward Label and Data2
            PdfPCell zooningLabel = GetCell("Zooning", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell zooningData = GetCell(facility.Zoning, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(zooningLabel);
            table.AddCell(zooningData);

            //Owner Label and Data
            PdfPCell ownerLabel = GetCell("Owner", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell ownerData = GetCell(facility.DeedsInfo.OwnerInfomation, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(ownerLabel);
            table.AddCell(ownerData);

            //Extent Label and Data2
            PdfPCell extentLabel = GetCell("Extent", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell extentData = GetCell(facility.DeedsInfo.Extent, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(extentLabel);
            table.AddCell(extentData);

            //User Label and Data
            PdfPCell userLabel = GetCell("User", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell userData = GetCell("facility.ResposiblePerson.FullName", BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(userLabel);
            table.AddCell(userData);

            //Extent Label and Data
            PdfPCell contactLabel = GetCell("Contact", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell contactData = GetCell("facility.ResposiblePerson.EmailAddress", BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(contactLabel);
            table.AddCell(contactData);

            return table;
        }

        private PdfPTable GetThirdTable(Facility facility)
        {
            //Second Table
            PdfPTable table = new PdfPTable(4)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                WidthPercentage = 90
            };

            //Label for Access Card
            var emptyCell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_TOP,
                HorizontalAlignment = Element.ALIGN_LEFT,
                BackgroundColor = BaseColor.GRAY,
                BorderColor = BaseColor.DARK_GRAY,
                Colspan = 4,
                MinimumHeight = 20
            };

            var idPhotoCell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_TOP,
                HorizontalAlignment = Element.ALIGN_LEFT,
                BackgroundColor = BaseColor.WHITE,
                BorderColor = BaseColor.DARK_GRAY,
                Colspan = 2,
                MinimumHeight = 120
            };
            idPhotoCell.AddElement(new Phrase("ID Photo",
                FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));

            var locationCell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_TOP,
                HorizontalAlignment = Element.ALIGN_LEFT,
                BackgroundColor = BaseColor.WHITE,
                BorderColor = BaseColor.DARK_GRAY,
                Colspan = 2,
                MinimumHeight = 120
            };
            locationCell.AddElement(new Phrase("Locaton (Google)",
                FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));

            var sketachCell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_TOP,
                HorizontalAlignment = Element.ALIGN_LEFT,
                BackgroundColor = BaseColor.WHITE,
                BorderColor = BaseColor.DARK_GRAY,
                Colspan = 4,
                MinimumHeight = 120
            };
            sketachCell.AddElement(new Phrase("Sketch",
                FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));
            table.AddCell(emptyCell);
            table.AddCell(idPhotoCell);
            table.AddCell(locationCell);
            table.AddCell(sketachCell);

            return table;
        }

        private PdfPTable GetFourthTable(Facility updatedfacility, Facility oldFacility = null)
        {
            //Second Table
            PdfPTable table = new PdfPTable(4)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                WidthPercentage = 90
            };

            //Base Infomation
            PdfPCell baseInfoCell = GetCell("BASE INFORMATION", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            baseInfoCell.Colspan = 2;
            baseInfoCell.MinimumHeight = 15;
            table.AddCell(baseInfoCell);

            //Location/Address Label and Data

            PdfPCell verficationInfoCell = GetCell("VERIFICATION FINDINGS", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            verficationInfoCell.Colspan = 2;
            verficationInfoCell.MinimumHeight = 15;
            table.AddCell(verficationInfoCell);

            //GPS Co-Ordinates Label and Data
            string facilityLatitude = updatedfacility.Location.GPSCoordinates != null ? updatedfacility.Location.GPSCoordinates.Latitude : string.Empty;
            string facilityLongitude = updatedfacility.Location.GPSCoordinates != null ? updatedfacility.Location.GPSCoordinates.Longitude : string.Empty;
            PdfPCell bGpsLabel = GetCell("GPS Co-Ordinates", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bGpsData = GetCell(string.Format("Latitude {0} \n Longitude {1}",
                facilityLatitude, facilityLongitude), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bGpsLabel);
            table.AddCell(bGpsData);

            PdfPCell aGpsLabel = GetCell("GPS Co-Ordinates", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aGpsData = GetCell(string.Format("Latitude {0} \n Longitude {1}", facilityLatitude,
                facilityLongitude), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aGpsLabel);
            table.AddCell(aGpsData);

            //Usage Label and Data
            PdfPCell bUsageLabel = GetCell("Usage", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bUsageData = GetCell(updatedfacility.Status, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bUsageLabel);
            table.AddCell(bUsageData);

            PdfPCell aUsageLabel = GetCell("Usage", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aUsageData = GetCell(updatedfacility.Status, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aUsageLabel);
            table.AddCell(aUsageData);

            //"No. Improvements Label and Data
            PdfPCell bImprovementLabel = GetCell("No. Improvements", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bImprovementData = GetCell(string.Format("{0}", updatedfacility.Buildings.Capacity), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bImprovementLabel);
            table.AddCell(bImprovementData);

            PdfPCell aImprovementLabel = GetCell("No. Improvements", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aImprovementData = GetCell(string.Format("{0}", updatedfacility.Buildings.Capacity), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aImprovementLabel);
            table.AddCell(aImprovementData);

            //Local Municipality Label and Data
            PdfPCell bImproveSizeLabel = GetCell("Improvements Size (M2)", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bImproveSizeData = GetCell(string.Format("{0}", updatedfacility.Buildings.Capacity), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bImproveSizeLabel);
            table.AddCell(bImproveSizeData);

            PdfPCell aImproveSizeLabel = GetCell("Improvements Size (M2)", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aImproveSizeData = GetCell(string.Format("{0}", updatedfacility.Buildings.Capacity), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aImproveSizeLabel);
            table.AddCell(aImproveSizeData);

            //Occupation Status (Capacity) Label and Data
            PdfPCell bOccStatusLabel = GetCell("Occupation Status (Capacity)", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bOccStatusData = GetCell(updatedfacility.Status, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bOccStatusLabel);
            table.AddCell(bOccStatusData);

            PdfPCell aOccStatusLabel = GetCell("Occupation Status (Capacity)", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aOccStatusData = GetCell(updatedfacility.Status, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aOccStatusLabel);
            table.AddCell(aOccStatusData);

            int i = 1;
            foreach (var building in updatedfacility.Buildings)
            {
                PdfPCell bPolygonLabel = GetCell("Boundary Polygon " + i, BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
                PdfPCell bPolygonData = GetCell(string.Format("Latitude {0} \n Longitude {1}",
                building.GPSCoordinates.Latitude, building.GPSCoordinates.Longitude), BaseColor.BLACK, BaseColor.WHITE);

                table.AddCell(bPolygonLabel);
                table.AddCell(bPolygonData);

                PdfPCell aPolygonLabel = GetCell("Boundary Polygon " + i, BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
                PdfPCell aPolygonData = GetCell(string.Format("Latitude {0} \n Longitude {1}",
                building.GPSCoordinates.Latitude, building.GPSCoordinates.Longitude), BaseColor.BLACK, BaseColor.WHITE);

                table.AddCell(aPolygonLabel);
                table.AddCell(aPolygonData);
                i++;
            }
            //Comments
            var commentsCell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_TOP,
                HorizontalAlignment = Element.ALIGN_LEFT,
                BackgroundColor = BaseColor.WHITE,
                BorderColor = BaseColor.DARK_GRAY,
                Colspan = 4,
                MinimumHeight = 120
            };
            commentsCell.AddElement(new Phrase("Comments",
                FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));
            table.AddCell(commentsCell);

            return table;
        }

        private PdfPCell GetCell(string data, BaseColor baseColor, BaseColor backgroundColor, int style = Font.NORMAL)
        {
            PdfPCell cell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.GRAY,
                BackgroundColor = backgroundColor,
            };
            cell.AddElement(new Phrase(data,
                FontFactory.GetFont(FontFactory.HELVETICA, 10, style, baseColor)));

            return cell;
        }
    }
}
