use TopBookStore;

-- select statement for TopBookStore tables
select *
from Customers;

select *
from Categories;

select *
from Books;

select *
from BookCategory;

select *
from Authors;

select *
from Carts;

select *
from CartItems;

select *
from OrderDetails;

select *
from Orders;

-- select statement for TopBookStore Identity tables
select *
from AspNetUsers;

select *
from AspNetRoles;

select *
from AspNetUserRoles;

delete from BookCategory;

delete from Books;
dbcc CHECKIDENT (Books, RESEED, 0)

delete from OrderDetails;
dbcc CHECKIDENT (OrderDetails, RESEED, 0)

delete from CartItems;
dbcc CHECKIDENT (CartItems, RESEED, 0)

delete from Orders;
dbcc CHECKIDENT (Orders, RESEED, 0)

-- delete Customers
delete from Customers
where FirstName != 'Dong';
dbcc CHECKIDENT (Customers, RESEED, 1)

delete from Customers;
dbcc CHECKIDENT (Customers, RESEED, 0)

delete from Carts;
dbcc CHECKIDENT (Carts, RESEED, 0)

delete from Categories;
dbcc CHECKIDENT (Categories, RESEED, 0)

delete from Publishers;
dbcc CHECKIDENT (Publishers, RESEED, 0)

delete from Authors
dbcc CHECKIDENT (Authors, RESEED, 0)

-- delete AspNetUsers
delete from AspNetUsers
where UserName != 'admin@gmail.com';

delete from AspNetUsers;

-- Inserting data into Authors table
insert into Authors
    (FirstName, LastName, PhoneNumber)
values
    (N'Nguyễn', N'Trần', '0987654321'),
    (N'Lê', N'Phạm', '0912345678'),
    (N'Trần', N'Nguyễn', null);

-- Inserting data into Categories table
insert into Categories
    (Name)
values
    (N'Tình Cảm'),
    (N'Hành Động'),
    (N'Phiêu Lưu'),
    (N'Khoa Học'),
    (N'Trinh Thám'),
    (N'Chính Trị'),
    (N'Kinh Doanh'),
    (N'Tâm Lý'),
    (N'Truyện Ngắn'),
    (N'Hài Hước');

-- Inserting data into Publishers table
insert into Publishers
    (Name)
values
    (N'Nhà xuất bản Kim Đồng'),
    (N'Nhà xuất bản Trẻ'),
    (N'Nhà xuất bản Văn học');

-- Inserting data into Books table
insert into Books
    (AuthorId, PublisherId, Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PublicationDate)
values
    -- Inserting 10 additional records
    (1, 1, N'Những Ngày Cuối Cùng', N'Một hành trình khám phá về cuộc sống trước ngày tận thế.', '9787654321098', 25, 95000, 0.15, 180, '2022-05-28'),
    (2, 2, N'Bí Mật Của Ngọn Đồi Xanh', N'Trinh thám hấp dẫn với những bí mật tại một ngôi làng nhỏ.', '9786543210987', 40, 110000, 0.12, 220, '2022-11-05'),
    (3, 3, N'Tiếng Gọi Của Biển Cả', N'Một hành trình phiêu lưu đầy kỳ bí qua đại dương.', '9789876543210', 15, 135000, 0.2, 300, '2023-07-15'),
    (1, 1, N'Bí Ẩn Trong Rừng Cổ', N'Khám phá bí mật của rừng cổ, nơi ẩn chứa những điều kỳ bí nhất.', '9783210987654', 28, 88000, 0.18, 190, '2022-03-02'),
    (2, 2, N'Cuộc Phiêu Lưu Của Alice', N'Một câu chuyện kỳ ảo với những nhân vật độc đáo.', '9781098765432', 35, 105000, 0.25, 160, '2023-01-20'),
    (3, 3, N'Hành Trình Đến Kỳ Quan Thế Giới', N'Khám phá về những kỳ quan nổi tiếng trên thế giới.', '9785432109876', 22, 125000, 0.1, 240, '2022-08-10'),
    (1, 1, N'Tình Yêu Bất Tận', N'Một câu chuyện tình lãng mạn đẹp như tranh.', '9782109876543', 18, 99000, 0.22, 200, '2023-04-18'),
    (2, 2, N'Hòn Đảo Hoang Sơ', N'Cuộc phiêu lưu kỳ thú trên hòn đảo hoang sơ.', '9784321098765', 32, 115000, 0.17, 170, '2021-12-30'),
    (3, 3, N'Đêm Trăng Lạnh', N'Một câu chuyện buồn về tình yêu và lạc lõng.', '9787654321098', 20, 102000, 0.2, 210, '2023-02-14'),
    (1, 1, N'Trở Về Quê Hương', N'Hành trình tìm kiếm gốc rễ và quê hương.', '9786543210987', 25, 95000, 0.15, 180, '2022-06-05'),
    (2, 2, N'Góc Nhìn Từ Tương Lai', N'Nhìn nhận về thế giới thông qua góc nhìn của tương lai.', '9788765432109', 18, 98000, 0.2, 220, '2023-09-15'),
    (3, 3, N'Bước Chân Trên Mặt Trăng', N'Khám phá về cuộc phiêu lưu đầu tiên lên mặt trăng.', '9787654321098', 30, 115000, 0.12, 180, '2022-04-02'),
    (1, 1, N'Tiếng Hát Từ Khu Rừng Sâu', N'Một câu chuyện về âm nhạc và tình bạn trong rừng sâu.', '9786543210987', 25, 105000, 0.18, 200, '2023-05-20'),
    (2, 2, N'Trò Chơi Của Thời Gian', N'Một thế giới mà thời gian không còn ràng buộc.', '9781098765432', 22, 110000, 0.15, 250, '2022-10-10'),
    (3, 3, N'Đại Dương Vô Tận', N'Khám phá về đại dương với những sinh vật kỳ diệu.', '9789876543210', 28, 120000, 0.2, 280, '2023-03-08'),
    (1, 1, N'Đêm Trước Giáng Sinh', N'Một câu chuyện ấm áp về tình yêu và hy vọng.', '9783210987654', 20, 95000, 0.22, 190, '2022-12-24'),
    (2, 2, N'Hành Trình Đến Vương Quốc Phép Thuật', N'Một cuộc phiêu lưu đến vương quốc phép thuật đầy màu sắc.', '9786543210987', 35, 115000, 0.1, 210, '2022-07-12'),
    (3, 3, N'Chạm Nhẹ Quá Khứ', N'Một hành trình nhìn lại quá khứ để tìm hiểu về bản thân.', '9785432109876', 15, 98000, 0.25, 170, '2023-01-05'),
    (1, 1, N'Trên Đỉnh Thế Giới', N'Đạt đến đỉnh thế giới với những khám phá mới.', '9782109876543', 32, 125000, 0.15, 240, '2022-08-28'),
    (2, 2, N'Cuộc Chiến Giữa Các Vì Sao', N'Cuộc chiến không gian giữa các hành tinh.', '9784321098765', 18, 99000, 0.2, 200, '2023-06-17'),
    (3, 3, N'Đường Mây Trên Cánh Đồng', N'Một hành trình tìm kiếm niềm vui giữa thiên nhiên.', '9787654321098', 28, 102000, 0.18, 180, '2022-02-08'),
    (1, 1, N'Thế Giới Cổ Tích', N'Một cuộc phiêu lưu qua các câu chuyện cổ tích.', '9786543210987', 20, 95000, 0.22, 210, '2023-04-02'),
    (2, 2, N'Trở Về Nơi Bắt Đầu', N'Hành trình trở về nơi mà mọi thứ bắt đầu.', '9781098765432', 22, 110000, 0.15, 180, '2022-11-15'),
    (3, 3, N'Đại Lộ Sách Vào Tương Lai', N'Khám phá về cách sách thay đổi cuộc sống trong tương lai.', '9789876543210', 25, 105000, 0.2, 220, '2023-07-01'),
    (1, 1, N'Góc Nhìn Từ Dưới Biển', N'Cuộc phiêu lưu dưới đáy biển với những sinh vật biển độc đáo.', '9783210987654', 30, 120000, 0.12, 200, '2022-05-10');

-- Inserting data into BookCategory table
insert into BookCategory
    (CategoryId, BookId)
values
    (1, 1),
    (2, 1),
    (2, 2),
    (3, 3);