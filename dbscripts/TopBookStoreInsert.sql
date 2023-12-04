use TopBookStore;

-- Inserting data into Authors table
insert into Authors
    (FirstName, LastName, PhoneNumber)
values
    (N'Nguyễn', N'Trần', '0987654321'),
    (N'Lê', N'Phạm', '0912345678'),
    (N'Trần', N'Nguyễn', null);

-- Inserting data into Publishers table
insert into Publishers
    (Name)
values
    (N'Nhà xuất bản Kim Đồng'),
    (N'Nhà xuất bản Trẻ'),
    (N'Nhà xuất bản Văn học');

-- Inserting data into Categories table
insert into Categories
    (Name)
values
    (N'Tiểu thuyết'),
    (N'Khoa học viễn tưởng'),
    (N'Kinh doanh và tài chính'),
    (N'Hồi ký và tự truyện'),
    (N'Tiểu sử'),
    (N'Hài hước'),
    (N'Tiểu thuyết lịch sử'),
    (N'Khoa học'),
    (N'Phiêu lưu'),
    (N'Tâm lý học'),
    (N'Huyền bí và siêu nhiên'),
    (N'Trinh thám'),
    (N'Lãng mạn'),
    (N'Kỹ năng sống'),
    (N'Lịch sử'),
    (N'Văn học trẻ em'),
    (N'Công nghệ và khoa học máy tính'),
    (N'Tôn giáo và tư tưởng'),
    (N'Y học và sức khỏe'),
    (N'Nghệ thuật và thiết kế'),
    (N'Văn hóa và xã hội'),
    (N'Tự nhiên và địa lý'),
    (N'Âm nhạc'),
    (N'Thể thao và thể dục');

-- Inserting data into Books table
insert into Books
    (AuthorId, PublisherId, Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PublicationDate)
values
    (1, 1, N'Những Ngày Cuối Cùng Của Hitler', N'Một hành trình khám phá về cuộc sống trước ngày tận thế.', '9787654321098', 25, 95000, 0.15, 180, '2022-05-28'),
    (2, 2, N'Đời Sống Bí Ẩn Của Cây', N'Trinh thám hấp dẫn với những bí mật tại một ngôi làng nhỏ.', '9786543210987', 40, 110000, 0.12, 220, '2022-11-05'),
    (3, 3, N'Ông già và biển cả', N'Một hành trình phiêu lưu đầy kỳ bí qua đại dương.', '9789876543210', 15, 135000, 0.2, 300, '2023-07-15'),
    (1, 1, N'Bí Ẩn Khu Rừng Già', N'Khám phá bí mật của rừng cổ, nơi ẩn chứa những điều kỳ bí nhất.', '9783210987654', 28, 88000, 0.18, 190, '2022-03-02'),
    (2, 2, N'Alice ở xứ sở diệu kỳ', N'Một câu chuyện kỳ ảo với những nhân vật độc đáo.', '9781098765432', 35, 105000, 0.25, 160, '2023-01-20'),
    (3, 3, N'Hành Trình Đến Kỳ Quan Thế Giới', N'Khám phá về những kỳ quan nổi tiếng trên thế giới.', '9785432109876', 22, 125000, 0.1, 240, '2022-08-10'),
    (1, 1, N'Tình Yêu Bất Tận', N'Một câu chuyện tình lãng mạn đẹp như tranh.', '9782109876543', 18, 99000, 0.22, 200, '2023-04-18'),
    (3, 3, N'Đêm Trăng Lạnh', N'Một câu chuyện buồn về tình yêu và lạc lõng.', '9787654321098', 20, 102000, 0.2, 210, '2023-02-14'),
    (1, 1, N'Trở Về Quê Hương', N'Hành trình tìm kiếm gốc rễ và quê hương.', '9786543210987', 25, 95000, 0.15, 180, '2022-06-05'),
    (1, 1, N'Tiếng Hát Từ Khu Rừng Sâu', N'Một câu chuyện về âm nhạc và tình bạn trong rừng sâu.', '9786543210987', 25, 105000, 0.18, 200, '2023-05-20'),
    (2, 2, N'Darkly Dreaming Dexter', N'Thế giới lạ lùng của con quái vật Dexter', '9781098765432', 22, 110000, 0.15, 250, '2022-10-10');

INSERT INTO Books
    (AuthorId, PublisherId, Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PublicationDate, ImageUrl)
VALUES
    (1, 1, N'Clean Code', N'Cuốn sách này giúp độc giả hiểu về các nguyên tắc và kỹ thuật viết code sạch, dễ đọc và dễ bảo trì trong phát triển phần mềm.', '1234567890123', 10, 150000, 0.10, 400, '2023-01-01', NULL),
    (2, 2, N'Clean Architecture', N'Cuốn sách này giúp độc giả hiểu về kiến trúc phần mềm sạch, tách biệt và dễ bảo trì, giúp tăng tính linh hoạt và khả năng mở rộng của hệ thống.', '2345678901234', 15, 180000, 0.15, 300, '2023-02-01', NULL),
    (3, 3, N'Đắc Nhân Tâm', N'Cuốn sách kinh điển này giúp độc giả hiểu về tâm lý con người và cung cấp những nguyên tắc để giao tiếp và tương tác hiệu quả với người khác.', '3456789012345', 12, 120000, 0.05, 350, '2023-03-01', NULL),
    (1, 2, N'Dế Mèn Phiêu Lưu Ký', N'Truyện kể về cuộc phiêu lưu của chú Dế Mèn và những người bạn, mang đến những bài học về tình bạn và lòng dũng cảm.', '4567890123456', 8, 90000, 0.20, 250, '2023-04-01', NULL),
    (2, 3, N'Giáo Trình Triết Học Mác - Lênin', N'Cuốn giáo trình này giúp độc giả hiểu về triết học Mác - Lênin và tư tưởng chủ nghĩa xã hội.', '5678901234567', 20, 135000, 0.10, 280, '2023-05-01', NULL),
    (3, 1, N'Bí Mật Của Phụ Nữ', N'Cuốn sách này khám phá về cách hiểu và tôn trọng phụ nữ, cung cấp những gợi ý và lời khuyên để tạo ra một mối quan hệ tốt đẹp', '6789012345678', 15, 110000, 0.15, 200, '2023-06-01', NULL),
    (1, 2, N'Chiếc Lược Ngà', N'Truyện kể về cuộc phiêu lưu của một người đàn ông tìm kiếm chiếc lược ngà huyền thoại trong một thế giới ma thuật.', '7890123456789', 10, 80000, 0.10, 150, '2023-07-01', NULL),
    (2, 3, N'Alibaba và 40 Tên Cướp', N'Truyện kể về cuộc phiêu lưu của Alibaba và nhóm 40 tên cướp, đặt trong bối cảnh của câu chuyện nổi tiếng "Ngàn và một đêm".', '8901234567890', 18, 140000, 0.20, 320, '2023-08-01', NULL),
    (3, 1, N'Tắt Đèn', N'Tác phẩm được xem là một trong những tiểu thuyết quan trọng nhất trong văn học Việt Nam, kể về cuộc đời và tình yêu của nhân vật chính.', '9012345678901', 13, 100000, 0.10, 280, '2023-09-01', NULL),
    (1, 2, N'Cho Tôi Xin Một Vé Đi Tuổi Thơ', N'Cuốn sách này đưa độc giả trở lại tuổi thơ và mang đến những kỷ niệm đáng nhớ qua câu chuyện và hình ảnh.', '0123456789012', 9, 75000, 0.15, 200, '2023-10-01', NULL),
    (2, 3, N'Trí Tuệ Do Thái', N'Cuốn sách này tìm hiểu về lịch sử và văn hóa của người Do Thái, cung cấp cái nhìn sâu sắc về trí tuệ và thành công của họ.', '1234567890123', 15, 130000, 0.05, 350, '2023-11-01', NULL),
    (3, 1, N'Truyện Kiều', N'Tác phẩm của nhà thơ Nguyễn Du, kể về cuộc đời và tình yêu đầy bi thương của nữ nhân vật Kiều.', '2345678901234', 12, 95000, 0.10, 320, '2023-12-01', NULL);

INSERT INTO Books
    (AuthorId, PublisherId, Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PublicationDate, ImageUrl)
VALUES
    (1, 1, N'Designing Machine Learning Systems', N'This book provides an in-depth understanding of designing machine learning systems, covering various concepts and best practices for building robust and scalable ML systems.', '3456789012345', 20, 180000, 0.10, 400, '2023-01-01', NULL),
    (2, 2, N'Xách Ba Lô Lên Và Đi', N'Truyện kể về hành trình phiêu lưu của một nhóm bạn trẻ, khám phá thế giới và tìm hiểu về cuộc sống.', '4567890123456', 15, 150000, 0.15, 300, '2023-02-01', NULL),
    (3, 3, N'Giấc Mơ Mỹ - Đường Đến Stanford', N'Cuốn sách này kể về câu chuyện cuộc sống và hành trình đến Mỹ, từ việc chuẩn bị cho học bổng đến việc thích nghi với môi trường học tập mới.', '5678901234567', 12, 120000, 0.05, 350, '2023-03-01', NULL),
    (1, 2, N'Đi Tìm Lẽ Sống', N'Cuốn sách này khám phá về ý nghĩa của cuộc sống và cung cấp những gợi ý và lời khuyên để tìm ra lẽ sống của riêng mình.', '6789012345678', 8, 90000, 0.20, 250, '2023-04-01', NULL),
    (2, 3, N'Bố Già', N'Cuốn sách kể về câu chuyện của một người đàn ông trưởng thành, cung cấp những bài học về gia đình, tình yêu và cuộc sống.', '7890123456789', 20, 135000, 0.10, 280, '2023-05-01', NULL),
    (3, 1, N'Mắt Biếc', N'Truyện kể về tình yêu và cuộc sống của nhân vật chính trong một ngôi làng miền Trung xưa.', '8901234567890', 15, 110000, 0.15, 200, '2023-06-01', NULL),
    (1, 2, N'Ngày Xưa Có Một Chuyện Tình', N'Truyện kể về một câu chuyện tình yêu lãng mạn, với những biến cố và thử thách đầy cảm xúc.', '9012345678901', 10, 80000, 0.10, 150, '2023-07-01', NULL),
    (2, 3, N'Đồi Gió Hú', N'Truyện kể về cuộc sống và tình yêu của nhân vật chính trong một thôn xóm miền núi.', '0123456789012', 18, 140000, 0.20, 320, '2023-08-01', NULL),
    (3, 1, N'Thiên Tài Bên Trái, Kẻ Điên Bên Phải', N'Cuốn sách khám phá về sự khác biệt và tầm quan trọng của trí tuệ đa dạng trong xã hội.', '1234567890123', 13, 100000, 0.10, 280, '2023-09-01', NULL);

INSERT INTO Books
    (AuthorId, PublisherId, Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PublicationDate, ImageUrl)
VALUES
    (1, 1, N'Head First Java', N'A beginner-friendly book that introduces the Java programming language in an interactive and engaging way.', '9780596009205', 10, 150000, 0.15, 500, '2021-07-01', NULL),
    (2, 2, N'Head First Design Patterns', N'A guide to understanding and implementing design patterns in software development.', '9780596007126', 8, 120000, 0.10, 400, '2022-03-01', NULL),
    (3, 3, N'Nhà giả kim', N'Cuốn sách nổi tiếng của Paulo Coelho về tìm kiếm ý nghĩa cuộc sống và sự trưởng thành cá nhân.', '9780062315007', 15, 90000, 0.20, 250, '2020-09-01', NULL),
    (1, 1, N'Người Giàu Có Nhất Thành Babylon', N'Một cuốn sách kinh điển về cách làm giàu và quản lý tài chính.', '9781604591870', 5, 80000, 0.10, 200, '2019-05-01', NULL),
    (2, 2, N'English Grammar in Use', N'A comprehensive guide to English grammar for learners of all levels.', '9781108457651', 12, 95000, 0.15, 300, '2018-11-01', NULL),
    (3, 3, N'Thao túng tâm lý', N'Tổng hợp các phương pháp thao túng tâm lý và tư duy của con người.', '9786041085410', 7, 70000, 0.05, 150, '2020-02-01', NULL),
    (1, 1, N'Làm Bạn Với Bầu Trời', N'Cuốn sách tâm lý học giúp bạn thấu hiểu bản thân và tìm kiếm ý nghĩa trong cuộc sống.', '9786042043307', 3, 60000, 0.10, 100, '2021-10-01', NULL),
    (2, 2, N'Hiểu Bản Thân, Quên Bản Thân', N'Cuốn sách tâm lý học giúp bạn hiểu rõ bản thân và thúc đẩy quá trình phát triển cá nhân.', '9786042073793', 9, 75000, 0.20, 250, '2019-07-01', NULL),
    (2, 1, N'Kính Vạn Hoa', N'Mô tả cho Kính Vạn Hoa', '3456789012345', 10, 150000, 0.00, 400, '2023-01-01', NULL),
    (3, 3, N'Cánh Đồng Bất Tận', N'Mô tả cho Cánh Đồng Bất Tận', '4567890123456', 15, 180000, 0.00, 350, '2023-02-01', NULL),
    (1, 2, N'Tâm trí tội phạm', N'Mô tả cho Tâm trí tội phạm', '5678901234567', 20, 200000, 0.00, 300, '2023-03-01', NULL),
    (1, 3, N'Harry Potter Và Phòng Chứa Bí Mật', N'Mô tả cho Harry Potter Và Phòng Chứa Bí Mật', '6789012345678', 25, 250000, 0.00, 500, '2023-04-01', NULL);

INSERT INTO Books
    (AuthorId, PublisherId, Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PublicationDate, ImageUrl)
VALUES
    (1, 1, N'Eloquent JavaScript', N'Mô tả cho Eloquent JavaScript', '1234567890123', 10, 100000, 0.00, 300, '2022-01-01', NULL),
    (2, 2, N'Don''t Make Me Think', N'Mô tả cho Don''t Make Me Think', '2345678901234', 15, 150000, 0.00, 250, '2022-02-01', NULL),
    (3, 3, N'Code Dạo Ký Sự', N'Mô tả cho Code Dạo Ký Sự', '3456789012345', 20, 200000, 0.00, 200, '2022-03-01', NULL),
    (1, 2, N'Hello Các Bạn Mình Là Tôi Đi Code Dạo', N'Mô tả cho Hello Các Bạn Mình Là Tôi Đi Code Dạo', '4567890123456', 12, 120000, 0.00, 350, '2022-04-01', NULL),
    (2, 3, N'Cây Cam Ngọt Của Tôi', N'Mô tả cho Cây Cam Ngọt Của Tôi', '5678901234567', 18, 180000, 0.00, 280, '2022-05-01', NULL),
    (3, 1, N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh', N'Mô tả cho Tôi Thấy Hoa Vàng Trên Cỏ Xanh', '6789012345678', 25, 250000, 0.00, 320, '2022-06-01', NULL),
    (1, 2, N'Mười Cô Gái Ngã ba Đồng Lộc', N'Mô tả cho Mười Cô Gái Ngã ba Đồng Lộc', '7890123456789', 14, 140000, 0.00, 290, '2022-07-01', NULL),
    (2, 3, N'Bên Nhau Trọn Đời', N'Mô tả cho Bên Nhau Trọn Đời', '8901234567890', 22, 220000, 0.00, 270, '2022-08-01', NULL),
    (3, 1, N'Giọt Lệ Trên Đời', N'Mô tả cho Giọt Lệ Trên Đời', '9012345678901', 16, 160000, 0.00, 310, '2022-09-01', NULL),
    (1, 2, N'Đất Rừng Phương Nam', N'Mô tả cho Đất Rừng Phương Nam', '0123456789012', 21, 210000, 0.00, 260, '2022-10-01', NULL),
    (2, 3, N'Khi Lỗi Thuộc Về Những Vì Sao', N'Mô tả cho Khi Lỗi Thuộc Về Những Vì Sao', '1234567890123', 17, 170000, 0.00, 330, '2022-11-01', NULL),
    (3, 1, N'Những Cô Gái Năm Ấy Chúng Ta Cùng Theo Đuổi', N'Mô tả cho Những Cô Gái Năm Ấy Chúng Ta Cùng Theo Đuổi', '2345678901234', 19, 190000, 0.00, 240, '2022-12-01', NULL);

-- Inserting data into BookCategory table
insert into BookCategory
    (CategoryId, BookId)
values
    (1, 1),
    (2, 1),
    (2, 2),
    (3, 3);