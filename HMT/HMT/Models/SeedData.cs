using HMT.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMT.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HMTContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<HMTContext>>()))
            {
                //Look for any students.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                //Category
                var categories = new Category[]
                {
                    new Category { NameCategory = "Stapler"},
                    new Category { NameCategory = "Tape"},
                    new Category { NameCategory = "Paper knives"},
                    new Category { NameCategory = "Hand sanitizer gel"},
                    new Category { NameCategory = "Paper"},
                    new Category { NameCategory = "Note"},
                    new Category { NameCategory = "Gum"},
                    new Category { NameCategory = "Drag"},
                    new Category { NameCategory = "Clip"},
                    new Category { NameCategory = "Leather book"},
                    new Category { NameCategory = "Spring book"},

                    new Category { NameCategory = "Ballpoint pens"},
                    new Category { NameCategory = "Premium ballpoint pen"},
                    new Category { NameCategory = "Pencil"},
                    new Category { NameCategory = "Eraser"},

                    new Category { NameCategory = "Cafe"},

                };

                foreach (Category u in categories)
                {
                    context.Categories.Add(u);
                }
                context.SaveChanges();

                //Product
                var products = new Product[]
                {
                    //Stapler
                    new Product { NameProduct = "Large stapler Fo-BS01", Description = "Flexoffice FO-BS01 stapler has material: Stainless steel, sturdy and durable to use. The press mechanism has a safety lock to avoid injuring the fingers when feeding the needle. The spring has good elasticity, durable to use. Smart design, light pressure, convenient and easy to use. Suitable for staples: 23/6, 23/8, 23/10, 23/13, 23/15 . needles." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Stapler").Id },
                    new Product { NameProduct = "Needle removal Fo-Str02", Description = "Flexoffice FO-BS01 stapler has material: Stainless steel, sturdy and durable to use. The press mechanism has a safety lock to avoid injuring the fingers when feeding the needle. The spring has good elasticity, durable to use. Smart design, light pressure, convenient and easy to use. Suitable for staples: 23/6, 23/8, 23/10, 23/13, 23/15 . needles." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Stapler").Id },
                    new Product { NameProduct = "Stapler NP.3 Fo-St01", Description = "Flexoffice FO-BS01 stapler has material: Stainless steel, sturdy and durable to use. The press mechanism has a safety lock to avoid injuring the fingers when feeding the needle. The spring has good elasticity, durable to use. Smart design, light pressure, convenient and easy to use. Suitable for staples: 23/6, 23/8, 23/10, 23/13, 23/15 . needles." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Stapler").Id },
                    //Taper
                    new Product { NameProduct = "Opp FO-BKD 20. Perforated tape", Description = "Thien Long OPP Tape - Flexoffice FO-BKD 20 is made from BOPP film with high strength, plus the coating glue selected to make the tape always ensures high adhesion, good elasticity." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Tape").Id },
                    new Product { NameProduct = "Opp FO-BKT 15 Inner tape", Description = "Thien Long OPP Tape - Flexoffice FO-BKD 20 is made from BOPP film with high strength, plus the coating glue selected to make the tape always ensures high adhesion, good elasticity." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Tape").Id },
                    new Product { NameProduct = "Opp perforated tape FO-BKD 08", Description = "Thien Long OPP Tape - Flexoffice FO-BKD 20 is made from BOPP film with high strength, plus the coating glue selected to make the tape always ensures high adhesion, good elasticity." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Tape").Id },
                    //Paperknivesr
                    new Product { NameProduct = "Paper Cutter Fo-KN04", Description = "Thien Long Paper Cutter - Flexoffice FO-KN04B has a blade made of carbon steel that is not rusty, scratched and has high durability, With high quality as well as a modern and convenient design, the product will be a learning tool. practice and work indispensable for students, students and office staff." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Paper knives").Id },
                    new Product { NameProduct = "Paper cutter blade FO-BL01", Description = "Thien Long Paper Cutter - Flexoffice FO-KN04B has a blade made of carbon steel that is not rusty, scratched and has high durability, With high quality as well as a modern and convenient design, the product will be a learning tool. practice and work indispensable for students, students and office staff." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Paper knives").Id },
                    new Product { NameProduct = "Paper cutter FO-KN04B", Description = "Thien Long Paper Cutter - Flexoffice FO-KN04B has a blade made of carbon steel that is not rusty, scratched and has high durability, With high quality as well as a modern and convenient design, the product will be a learning tool. practice and work indispensable for students, students and office staff." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Paper knives").Id },
                    //Hand sanitizer gel
                    new Product { NameProduct = "Fo-HG001 Hand sanitizer 50ml", Description = "FO-HG001 dry hand sanitizer gel with capacity of 50ml belongs to the 'New generation of Thien Long product' line, which is manufactured according to modern technology, meeting international standards, and being environmentally friendly.\r\nFO-HG001 hand sanitizer gel for clean hands without soap and water." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Hand sanitizer gel").Id },
                    new Product { NameProduct = "Hand sanitizer gel Fo-HG002 100ml", Description = "FO-HG001 dry hand sanitizer gel with capacity of 50ml belongs to the 'New generation of Thien Long product' line, which is manufactured according to modern technology, meeting international standards, and being environmentally friendly.\r\nFO-HG001 hand sanitizer gel for clean hands without soap and water." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Hand sanitizer gel").Id },
                    //Paper
                    new Product { NameProduct = "A4/80 PP-02 .Paper", Description = "Thien Long Paper - Flexoffice A4/80 PP-02 high quality, excellent smooth surface, quick-drying ink, suitable for inkjet printers, laser printers, photocopiers." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Paper").Id },
                    new Product { NameProduct = "A4/70 PP-01 .Paper", Description = "Thien Long Paper - Flexoffice A4/80 PP-02 high quality, excellent smooth surface, quick-drying ink, suitable for inkjet printers, laser printers, photocopiers." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Paper").Id },
                    //Note
                    new Product { NameProduct = "3x3 sticky notes FO-SN03", Description = "Thien Long sticky notes - Flexoffice 3x5 FO-SN05 helps you manage your organization in the long term. Includes many sheets in 1 pack, can be removed and glued easily." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Note").Id },
                    new Product { NameProduct = "3x4 sticky notes FO-SN04", Description = "Thien Long sticky notes - Flexoffice 3x5 FO-SN05 helps you manage your organization in the long term. Includes many sheets in 1 pack, can be removed and glued easily." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Note").Id },
                    new Product { NameProduct = "3x5 sticky notes FO-SN05", Description = "Thien Long sticky notes - Flexoffice 3x5 FO-SN05 helps you manage your organization in the long term. Includes many sheets in 1 pack, can be removed and glued easily." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Note").Id },
                    //Gum
                    new Product { NameProduct = "Gum Fo-E04", Description = "Flexoffice FO-E08 gum is made of high-quality materials, meets the allowed safety criteria, has no unpleasant odor, brings peace of mind to the user. The gum is super soft and super flexible, not hard when used for a long time." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Gum").Id },
                    new Product { NameProduct = "Gum Fo-E03", Description = "Flexoffice FO-E08 gum is made of high-quality materials, meets the allowed safety criteria, has no unpleasant odor, brings peace of mind to the user. The gum is super soft and super flexible, not hard when used for a long time." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Gum").Id },
                    new Product { NameProduct = "Gum Fo-E08", Description = "Flexoffice FO-E08 gum is made of high-quality materials, meets the allowed safety criteria, has no unpleasant odor, brings peace of mind to the user. The gum is super soft and super flexible, not hard when used for a long time." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Gum").Id },
                    //Drag
                    new Product { NameProduct = "Drag Fo-SC02", Description = "Thien Long Scissors - Flexoffice FO-SC05 is a scissor for the office, made of stainless metal, with a rounded tip to protect the safety of the user. Plastic handle for gentle, pain-free cutting." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Drag").Id },
                    new Product { NameProduct = "Drag Fo-SCO03", Description = "Thien Long Scissors - Flexoffice FO-SC05 is a scissor for the office, made of stainless metal, with a rounded tip to protect the safety of the user. Plastic handle for gentle, pain-free cutting." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Drag").Id },
                    new Product { NameProduct = "Drag Fo-SCO05", Description = "Thien Long Scissors - Flexoffice FO-SC05 is a scissor for the office, made of stainless metal, with a rounded tip to protect the safety of the user. Plastic handle for gentle, pain-free cutting." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Drag").Id },
                    //Clip
                    new Product { NameProduct = "51mm butterfly clamp FO-DC06", Description = "Thien Long Butterfly Clip - Flexoffice 51mm FO-DC06 is an indispensable item in every office to help organize papers and documents more neatly." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Clip").Id },
                    new Product { NameProduct = "Paperclip FO-PAC02", Description = "Thien Long Butterfly Clip - Flexoffice 51mm FO-DC06 is an indispensable item in every office to help organize papers and documents more neatly." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Clip").Id },
                    new Product { NameProduct = "51mm color butterfly clip FO-DCC06", Description = "Thien Long Butterfly Clip - Flexoffice 51mm FO-DC06 is an indispensable item in every office to help organize papers and documents more neatly." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Clip").Id },
                    //Leather book
                    //new Products { NameProduct = "Leather notebook FO-MB004", Description = "The FO-MB005 leather cover book is a product of the FlexOffice label manufactured by modern technology, meeting international standards, and being environmentally friendly." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Leather Book").Id },
                    //new Products { NameProduct = "Leather notebook FO-MB005", Description = "The FO-MB005 leather cover book is a product of the FlexOffice label manufactured by modern technology, meeting international standards, and being environmentally friendly." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Leather Book").Id },                   
                    //spring book
                    new Product { NameProduct = "Spring book FO-MBM001", Description = "The FO-MB003 spring notebook is a product of the FlexOffice label manufactured by modern technology, meeting international standards, and being environmentally friendly." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Spring book").Id },
                    new Product { NameProduct = "Spring book FO-MBM002", Description = "The FO-MB003 spring notebook is a product of the FlexOffice label manufactured by modern technology, meeting international standards, and being environmentally friendly." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Spring book").Id },
                    new Product { NameProduct = "Spring book FO-MBM003", Description = "The FO-MB003 spring notebook is a product of the FlexOffice label manufactured by modern technology, meeting international standards, and being environmentally friendly." ,IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Spring book").Id },

                    //Ballpoint pens
                    new Product { NameProduct = "Ballpoint pen FO-039", Description = "Senior FO-026 ballpoint pen is a product manufactured by Thien Long Group, branded FlexOffice.Press ballpoint pen. Design style is strong, youthful, dynamic and modern.",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Ballpoint pens").Id },
                    new Product { NameProduct = "Ballpoint pen Fo-026", Description = "Senior FO-026 ballpoint pen is a product manufactured by Thien Long Group, branded FlexOffice.Press ballpoint pen. Design style is strong, youthful, dynamic and modern.",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Ballpoint pens").Id },
                    new Product { NameProduct = "Ballpoint pen FO-039 Plus", Description = "Senior FO-026 ballpoint pen is a product manufactured by Thien Long Group, branded FlexOffice.Press ballpoint pen. Design style is strong, youthful, dynamic and modern.",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Ballpoint pens").Id },
                    //premium ballpoint pen
                    //new Products { NameProduct = "Premium ballpoint pen Fo-069/VN", Description = "High-end ballpoint pen Flexoffice FO-060/VN (rotating) is a high-end pen product line of Thien Long branded Flexoffice. The pen has a modern and luxurious design, suitable for teachers, office workers...",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Premium Ballpoint pen").Id },
                    //new Products { NameProduct = "Premium ballpoint pen FO-067/vn", Description = "High-end ballpoint pen Flexoffice FO-060/VN (rotating) is a high-end pen product line of Thien Long branded Flexoffice. The pen has a modern and luxurious design, suitable for teachers, office workers...",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Premium Ballpoint pen").Id },
                    //new Products { NameProduct = "Premium ballpoint pen FO-060/Vn", Description = "High-end ballpoint pen Flexoffice FO-060/VN (rotating) is a high-end pen product line of Thien Long branded Flexoffice. The pen has a modern and luxurious design, suitable for teachers, office workers...",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Premium Ballpoint pen").Id },
                    //pencil
                    new Product { NameProduct = "Wooden Pencil FO-GP06", Description = "The 2B Flexoffice FO-GP08 wooden pencil is suitable for activities such as taking notes, sketching, studying. Used to polish drawings, achieve different levels of light and dark. Also useful for highlighting the fastest multiple choice answer box.",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Pencil").Id },
                    new Product { NameProduct = "Wooden Pencil FO-GP07", Description = "The 2B Flexoffice FO-GP08 wooden pencil is suitable for activities such as taking notes, sketching, studying. Used to polish drawings, achieve different levels of light and dark. Also useful for highlighting the fastest multiple choice answer box.",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Pencil").Id },
                    new Product { NameProduct = "Wooden Pencil FO-GP08", Description = "The 2B Flexoffice FO-GP08 wooden pencil is suitable for activities such as taking notes, sketching, studying. Used to polish drawings, achieve different levels of light and dark. Also useful for highlighting the fastest multiple choice answer box.",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Pencil").Id },
                    //Eraser
                    new Product { NameProduct = "Eraser pen FO-CP02/vn", Description = "Thien Long eraser pen - FlexOffice FO-CP01 Plus has a flat body design, easy to reach and convenient to use",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Eraser").Id },
                    new Product { NameProduct = "Eraser pen FO-CP03/vn", Description = "Thien Long eraser pen - FlexOffice FO-CP01 Plus has a flat body design, easy to reach and convenient to use",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Eraser").Id },
                    new Product { NameProduct = "Eraser Pen FO-CT-02/vn", Description = "Thien Long eraser pen - FlexOffice FO-CP01 Plus has a flat body design, easy to reach and convenient to use",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Eraser").Id },

                    //cafe
                    new Product { NameProduct = "Peace Cafe", Description = "Trung Nguyen Coffee Coffee milk 3in1",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Cafe").Id },
                    new Product { NameProduct = "sachet Cafe", Description = "Trung Nguyen Coffee Coffee milk 3in1",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Cafe").Id },
                    new Product { NameProduct = "Millk Cafe", Description = "Trung Nguyen Coffee Coffee milk 3in1",IsDeleted = false, CategoryId  = categories.Single( a => a.NameCategory == "Cafe").Id },
                };

                foreach (Product p in products)
                {
                    context.Products.Add(p);
                }
                context.SaveChanges();

                //Resquests
                var requests = new Request[]
                {
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "4", UserManagerId = "3"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "4", UserManagerId = "3"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "4", UserManagerId = "3"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "4", UserManagerId = "3"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "4", UserManagerId = "2"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "5", UserManagerId = "2"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "5", UserManagerId = "3"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "5", UserManagerId = "2"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "5", UserManagerId = "2"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "3", UserManagerId = "1"},
                    new Request { Request_Status = '3', TotalQuantity = 2, TotalPrice = 4.5, Create_at = DateTime.Now, UserId  = "4", UserManagerId = "2"},

                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "3", UserManagerId = "1"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "3", UserManagerId = "1"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "4", UserManagerId = "2"},
                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "4", UserManagerId = "2"},

                    new Request { Request_Status = '0', TotalQuantity = 2, TotalPrice = 0, Create_at = DateTime.Now, UserId  = "5", UserManagerId = "2"},
                };

                foreach (Request p in requests)
                {
                    context.Requests.Add(p);
                }
                context.SaveChanges();

                //ResquestDetails
                var requestDetails = new RequestDetail[]
                {
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 1, RequestId = 1},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 2, RequestId = 2},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 3, RequestId = 3},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 4, RequestId = 4},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 5, RequestId = 5},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 6, RequestId = 6},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 7, RequestId = 7},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 8, RequestId = 8},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 9, RequestId = 9},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 9, RequestId = 10},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 4.5, Create_at = DateTime.Now, Note = "", ProductId  = 11, RequestId = 11},

                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 12, RequestId = 12},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 12, RequestId = 13},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 13, RequestId = 14},
                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 13, RequestId = 15},

                    new RequestDetail { Status = '0', Quantity = 2, Price = 0, Create_at = DateTime.Now, Note = "", ProductId  = 14, RequestId = 16},
                };

                foreach (RequestDetail p in requestDetails)
                {
                    context.RequestDetails.Add(p);
                }
                context.SaveChanges();

                //TotalProducts
                var totalProducts = new TotalProduct[]
                {
                    new TotalProduct {TotalQuantity = 2, TotalPrice = 4.5, ProductId = 11},
                };

                foreach (TotalProduct p in totalProducts)
                {
                    context.TotalProducts.Add(p);
                }
                context.SaveChanges();
            }
        }
    }
}
