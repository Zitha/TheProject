using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using TheProject.Model;

namespace TheProject.ReportGenerator
{
    public class FacilityReport
    {
        public string GenerateFacilityReport(Facility facility)
        {
            string _currpath = ConfigurationManager.AppSettings["ReportsPath"];
            //string _currpath = @"C:\Projects\TheProject\TheProject.Web\TheProject.ReportGenerator\Reports\";

            //_invoicepath = @"C:\Projects\Rustivia\IntroductionMVC5\IntroductionMVC5\ArsloInvoice";
            if (!Directory.Exists(_currpath))
            {
                Directory.CreateDirectory(_currpath);
            }

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
            fourthTable.SpacingBefore = 10f;
            fourthTable.SpacingAfter = 10f;
            //-----------------------//---------------------------------------------------//

            PdfPTable fithTable = GetFithTable(facility);

            doc.Add(mainTable);
            doc.Add(firstTable);
            doc.Add(secondTable);
            // doc.Add(firstTable);
            doc.Add(thirdTable);
            //doc.Add(firstTable);
            doc.Add(fourthTable);
            doc.Add(fithTable);

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
            invNumberCellData.AddElement(new Phrase(string.Format("User Department:\n Facilities Management and Real Estate"),
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

            //Label for Asset Card
            var accessCardCell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_MIDDLE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderColor = BaseColor.DARK_GRAY,
                Colspan = 4,
                PaddingLeft = 170
            };
            accessCardCell.AddElement(new Phrase("Asset Card",
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

            //Asset Identification Label
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
            accessIdentificationCellLbl.AddElement(new Phrase("Asset Identification",
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
            PdfPCell locationCellData = GetCell(string.Format("{1} \n{0}", facility.Location.Suburb, facility.Location.StreetAddress), BaseColor.BLACK, BaseColor.WHITE);

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
            PdfPCell zooningLabel = GetCell("Zoning", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
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
            string resposoblePersonName = facility.ResposiblePerson != null ? facility.ResposiblePerson.FullName : string.Empty;
            PdfPCell userLabel = GetCell("User", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell userData = GetCell(resposoblePersonName, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(userLabel);
            table.AddCell(userData);

            string resposoblePersonEmail = facility.ResposiblePerson != null ? facility.ResposiblePerson.FullName : string.Empty;
            PdfPCell contactLabel = GetCell("Contact", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell contactData = GetCell(resposoblePersonEmail, BaseColor.BLACK, BaseColor.WHITE);

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

            //Label for Asset Card
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

            List<Image> images = GetImages(facility.IDPicture);

            var locationCell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_TOP,
                HorizontalAlignment = Element.ALIGN_LEFT,
                BackgroundColor = BaseColor.WHITE,
                BorderColor = BaseColor.DARK_GRAY,
                Colspan = 2,
                MinimumHeight = 120
            };
            if (images.Count > 0)
            {
                images[0].ScaleToFit(300f, 120f);
                images[0].Alignment = 50;

                idPhotoCell.AddElement(images[0]);
            }
            else
            {
                idPhotoCell.AddElement(new Phrase("Photo",
               FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));
            }

            if (images.Count > 1)
            {
                images[1].ScaleToFit(300f, 120f);
                images[1].Alignment = 50;
                locationCell.AddElement(images[1]);
            }
            else
            {
                locationCell.AddElement(new Phrase("Sketch",
                        FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));
            }

            string imageUrl = "https://maps.googleapis.com/maps/api/staticmap?center="+ facility.Location.GPSCoordinates.Longitude + "," + facility.Location.GPSCoordinates.Latitude + "&zoom=12&size=600x200&maptype=roadmap&markers=color:red%7Clabel:S%7C" + facility.Location.GPSCoordinates.Longitude + "," + facility.Location.GPSCoordinates.Latitude + "&markers=color:green%7Clabel:G%7C" + facility.Location.GPSCoordinates.Longitude + "," + facility.Location.GPSCoordinates.Latitude + "&markers=color:red%7Clabel:C%7C" + facility.Location.GPSCoordinates.Longitude + "," + facility.Location.GPSCoordinates.Latitude + "&key=AIzaSyDBO8aaqwAq2x9_0uI-GtnQ4ulWxISpbiM";
            Image locatonImage = Image.GetInstance(imageUrl);
            var sketachCell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_TOP,
                HorizontalAlignment = Element.ALIGN_LEFT,
                BackgroundColor = BaseColor.WHITE,
                BorderColor = BaseColor.DARK_GRAY,
                Colspan = 4,
                MinimumHeight = 120
            };
            if (locatonImage !=null)
            {
                sketachCell.AddElement(locatonImage);
            }
            else
            {
                locationCell.AddElement(new Phrase("Location (Google)",
                        FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));
            }
            
          
            table.AddCell(emptyCell);
            table.AddCell(idPhotoCell);
            table.AddCell(locationCell);
            table.AddCell(sketachCell);

            return table;
        }

        private List<Image> GetImages(string iDPicture)
        {
            List<Image> images = new List<Image>();
            if (!string.IsNullOrEmpty(iDPicture))
            {
                string[] pictures = iDPicture.Split(',');
                for (int i = 0; i < pictures.Length; i++)
                {
                    string imagesLocation = ConfigurationManager.AppSettings["PicturesPath"];

                    if (File.Exists(imagesLocation + pictures[i] + ".png"))
                    {
                        images.Add(Image.GetInstance(imagesLocation + pictures[i] + ".png"));
                    }
                }
            }
            return images;
        }

        private PdfPTable GetFourthTable(Facility updatedfacility)
        {
            //Fourth Table
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

            PdfPCell bGpsLabel = GetCell("GPS Co-Ordinates", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bGpsData = GetCell(string.Format("Latitude: {0} \nLongitude: {1}",
                "", ""), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bGpsLabel);
            table.AddCell(bGpsData);

            string facilityLatitude = updatedfacility.Location.GPSCoordinates != null ? updatedfacility.Location.GPSCoordinates.Latitude : string.Empty;
            string facilityLongitude = updatedfacility.Location.GPSCoordinates != null ? updatedfacility.Location.GPSCoordinates.Longitude : string.Empty;
            PdfPCell aGpsLabel = GetCell("GPS Co-Ordinates", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aGpsData = GetCell(string.Format("Latitude: {0} \nLongitude: {1}", facilityLatitude,
                facilityLongitude), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aGpsLabel);
            table.AddCell(aGpsData);

            //Usage Label and Data
            PdfPCell bUsageLabel = GetCell("Usage", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bUsageData = GetCell("", BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bUsageLabel);
            table.AddCell(bUsageData);

            PdfPCell aUsageLabel = GetCell("Usage", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aUsageData = GetCell(updatedfacility.SettlementType, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aUsageLabel);
            table.AddCell(aUsageData);

            //"No. Improvements Label and Data
            PdfPCell bImprovementLabel = GetCell("No. Improvements", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bImprovementData = GetCell(string.Format("{0}", ""), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bImprovementLabel);
            table.AddCell(bImprovementData);

            PdfPCell aImprovementLabel = GetCell("No. Improvements", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aImprovementData = GetCell(string.Format("{0}", updatedfacility.Buildings.Capacity), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aImprovementLabel);
            table.AddCell(aImprovementData);

            //Local Municipality Label and Data
            PdfPCell bImproveSizeLabel = GetCell("Improvements Size (M2)", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bImproveSizeData = GetCell(string.Format("{0}", ""), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bImproveSizeLabel);
            table.AddCell(bImproveSizeData);

            PdfPCell aImproveSizeLabel = GetCell("Improvements Size (M2)", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aImproveSizeData = GetCell(string.Format("{0}", updatedfacility.Buildings.Capacity), BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aImproveSizeLabel);
            table.AddCell(aImproveSizeData);

            //Occupation Status (Capacity) Label and Data
            PdfPCell bOccStatusLabel = GetCell("Occupation Status (Capacity)", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell bOccStatusData = GetCell("", BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(bOccStatusLabel);
            table.AddCell(bOccStatusData);

            PdfPCell aOccStatusLabel = GetCell("Occupation Status (Capacity)", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell aOccStatusData = GetCell(updatedfacility.Status, BaseColor.BLACK, BaseColor.WHITE);

            table.AddCell(aOccStatusLabel);
            table.AddCell(aOccStatusData);


            for (int i = 0; i < 5; i++)
            {
                BoundryPolygon polygon = null;
                if (i < updatedfacility.Location.BoundryPolygon.Count)
                {
                    polygon = updatedfacility.Location.BoundryPolygon[i];
                }

                //string bpolygon = polygon != null ? string.Format("Latitude: {0} \nLongitude: {1}",
                //polygon.Latitude, polygon.Longitude) : "N/A";
                int count = i + 1;
                PdfPCell bPolygonLabel = GetCell("Boundary Polygon " + count, BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
                PdfPCell bPolygonData = GetCell("N/A", BaseColor.BLACK, BaseColor.WHITE);

                table.AddCell(bPolygonLabel);
                table.AddCell(bPolygonData);

                string apolygon = polygon != null ? string.Format("Latitude: {0} \nLongitude: {1}",
                polygon.Latitude, polygon.Longitude) : "N/A";
                PdfPCell aPolygonLabel = GetCell("Boundary Polygon " + count, BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
                PdfPCell aPolygonData = GetCell(apolygon, BaseColor.BLACK, BaseColor.WHITE);

                table.AddCell(aPolygonLabel);
                table.AddCell(aPolygonData);
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

        private PdfPTable GetFithTable(Facility updatedfacility, Facility oldFacility = null)
        {
            PdfPTable firstTable = new PdfPTable(1)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                WidthPercentage = 90
            };
            //Building Table
            PdfPTable table = new PdfPTable(6)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                WidthPercentage = 90
            };

            //Building Infomation
            PdfPCell baseInfoCell = GetCell("Improvement/Building Schedule", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            baseInfoCell.Colspan = 6;
            baseInfoCell.MinimumHeight = 15;

            table.AddCell(baseInfoCell);

            PdfPCell cell = new PdfPCell
            {
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BackgroundColor = BaseColor.WHITE
            };
            cell.AddElement(new Phrase(string.Empty, FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.WHITE)));
            cell.Colspan = 6;

            table.AddCell(cell);

            PdfPCell b1 = GetCell("No ", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell b2 = GetCell("Id ", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell b3 = GetCell("Name ", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell b4 = GetCell("Size(m2) ", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell b5 = GetCell("Photo ", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);
            PdfPCell b6 = GetCell("Sketch ", BaseColor.BLACK, BaseColor.LIGHT_GRAY, Font.BOLD);

            table.AddCell(b1);
            table.AddCell(b2);
            table.AddCell(b3);
            table.AddCell(b4);
            table.AddCell(b5);
            table.AddCell(b6);

            int i = 1;
            foreach (var building in updatedfacility.Buildings)
            {
                PdfPCell bb1 = GetCell(i.ToString(), BaseColor.BLACK, BaseColor.WHITE, Font.NORMAL);
                PdfPCell bb2 = GetCell(building.BuildingNumber.ToString(), BaseColor.BLACK, BaseColor.WHITE, Font.NORMAL);
                PdfPCell bb3 = GetCell(building.BuildingName.ToString(), BaseColor.BLACK, BaseColor.WHITE, Font.NORMAL);
                PdfPCell bb4 = GetCell(building.FootPrintArea.ToString(), BaseColor.BLACK, BaseColor.WHITE, Font.NORMAL);

                //Building Images
                List<Image> images = GetImages(building.Photo);
                PdfPCell bb5 = GetCell(string.Empty, BaseColor.BLACK, BaseColor.WHITE, Font.NORMAL);
                PdfPCell bb6 = GetCell(string.Empty, BaseColor.BLACK, BaseColor.WHITE, Font.NORMAL);

                if (images.Count>0)
                {
                    images[0].ScaleToFit(90f, 50f);
                    images[0].Alignment = 50;

                    bb5.AddElement(images[0]);
                }
                if (images.Count > 1)
                {
                    images[1].ScaleToFit(90f, 50f);
                    images[1].Alignment = 50;

                    bb6.AddElement(images[1]);
                }

                table.AddCell(bb1);
                table.AddCell(bb2);
                table.AddCell(bb3);
                table.AddCell(bb4);
                table.AddCell(bb5);
                table.AddCell(bb6);
                i++;
            }

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
