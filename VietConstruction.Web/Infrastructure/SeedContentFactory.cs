using VietConstruction.Web.Data.Entities;

namespace VietConstruction.Web.Infrastructure;

public static class SeedContentFactory
{
    public static SiteSetting CreateSiteSetting()
    {
        return new SiteSetting
        {
            CompanyName = "Công ty Cổ phần Xây dựng Việt Thành Công",
            Tagline = "Tổng thầu xây dựng công nghiệp và hạ tầng với quy trình quản trị chặt chẽ.",
            Address = "88 Nguyễn Văn Linh, Hải Châu, Đà Nẵng",
            Hotline = "0909 123 456",
            Email = "lienhe@vietthanhcong.vn",
            Website = "https://vietthanhcong.vn",
            WorkingHours = "Thứ 2 - Thứ 7, 08:00 - 17:30",
            FooterCredentials = "Chứng chỉ năng lực hoạt động xây dựng hạng I | ISO 9001:2015 | ISO 45001:2018",
            HeroTitle = "Đồng hành cùng chủ đầu tư bằng năng lực tổng thầu minh bạch và bền vững",
            HeroSummary = "Hệ thống CMS quản lý trang giới thiệu doanh nghiệp, hồ sơ năng lực, dự án, tin tức và tuyển dụng cho doanh nghiệp xây dựng."
        };
    }

    public static IReadOnlyList<CmsPage> CreatePages()
    {
        return new[]
        {
            CreatePage("trang-chu", "Trang chủ", "Trang chủ", "home", 0, true, "Tổng thầu xây dựng công nghiệp và hạ tầng", "Việt Thành Công cung cấp dịch vụ tổng thầu, thi công kết cấu, cơ điện và hoàn thiện cho các dự án công nghiệp, thương mại và hạ tầng.", "<p>Việt Thành Công tập trung vào quản trị tiến độ, an toàn lao động, pháp lý hồ sơ và kiểm soát chất lượng xuyên suốt vòng đời dự án.</p>"),
            CreatePage("gioi-thieu", "Giới thiệu", "Giới thiệu", "page", 1, true, "Giới thiệu doanh nghiệp", "Thông tin tổng quan về định hướng phát triển, mô hình quản trị và cam kết thực thi của doanh nghiệp.", "<p>Doanh nghiệp phát triển từ đội ngũ kỹ sư và quản lý dự án có nhiều năm kinh nghiệm trong lĩnh vực xây dựng công nghiệp và hạ tầng kỹ thuật.</p>"),
            CreatePage("co-cau-to-chuc", "Cơ cấu tổ chức", "Cơ cấu tổ chức", "page", 2, true, "Cơ cấu tổ chức điều hành", "Bộ máy tổ chức được thiết kế theo hướng chuyên môn hóa, kiểm soát trách nhiệm và phối hợp liên phòng ban.", "<p>Khối điều hành, kỹ thuật, QS/QC, an toàn, mua sắm và vận hành công trường được phân quyền rõ ràng theo từng giai đoạn thực hiện dự án.</p>"),
            CreatePage("nang-luc-nha-thau", "Năng lực nhà thầu", "Năng lực nhà thầu", "page", 3, true, "Năng lực nhà thầu", "Năng lực tổng hợp về pháp lý, tài chính, thiết bị, nhân sự và kinh nghiệm thi công của doanh nghiệp.", "<p>Doanh nghiệp đáp ứng yêu cầu của các chủ đầu tư trong nước và FDI thông qua hệ thống quản trị dự án, kiểm soát chất lượng và an toàn lao động nhất quán.</p>"),
            CreatePage("ho-so-nang-luc", "Hồ sơ năng lực", "Hồ sơ năng lực", "page", 4, true, "Hồ sơ năng lực doanh nghiệp", "Tập hợp hồ sơ giới thiệu, năng lực thực hiện và thành tích tiêu biểu trong các lĩnh vực xây dựng trọng điểm.", "<p>Hồ sơ năng lực được cập nhật định kỳ để phục vụ đấu thầu, chào thầu và giới thiệu doanh nghiệp với đối tác, chủ đầu tư.</p>"),
            CreatePage("ho-so-phap-ly", "Hồ sơ pháp lý", "Hồ sơ pháp lý", "page", 5, true, "Hồ sơ pháp lý minh bạch", "Giấy phép, chứng chỉ năng lực, chứng nhận hệ thống quản lý và hồ sơ tuân thủ pháp luật hiện hành.", "<p>Toàn bộ hồ sơ pháp lý, chứng chỉ năng lực hoạt động xây dựng và bảo hiểm công trình được lưu trữ đầy đủ, sẵn sàng đối soát khi triển khai dự án.</p>"),
            CreatePage("nang-luc-tai-chinh", "Năng lực tài chính", "Năng lực tài chính", "page", 6, true, "Năng lực tài chính ổn định", "Năng lực tài chính thể hiện qua hệ số thanh khoản, hạn mức tín dụng và khả năng huy động nguồn lực thi công.", "<p>Doanh nghiệp duy trì nguồn vốn lưu động và hạn mức tín dụng phục vụ thi công đồng thời nhiều dự án với kiểm soát dòng tiền chặt chẽ.</p>"),
            CreatePage("may-moc-thiet-bi", "Máy móc thiết bị", "Máy móc thiết bị", "page", 7, true, "Máy móc thiết bị chủ lực", "Danh mục thiết bị thi công phục vụ các hạng mục nền móng, kết cấu, hoàn thiện và cơ điện.", "<p>Hệ thống thiết bị gồm cẩu tháp, xe nâng, máy xúc, máy phát điện, cốp pha và thiết bị an toàn được kiểm định định kỳ theo quy chuẩn hiện hành.</p>"),
            CreatePage("du-an", "Dự án", "Dự án", "page", 8, true, "Danh mục dự án", "Tổng hợp các dự án đang thực hiện và đã hoàn thành trong lĩnh vực công nghiệp, thương mại và hạ tầng.", "<p>Danh mục dự án được phân loại rõ ràng để chủ đầu tư dễ dàng tra cứu năng lực triển khai theo quy mô, lĩnh vực và địa bàn.</p>"),
            CreatePage("tin-tuc", "Tin tức", "Tin tức", "page", 9, true, "Tin tức doanh nghiệp", "Cập nhật các hoạt động điều hành, tiến độ công trình, thông báo nội bộ và thông tin tuyển dụng.", "<p>Module tin tức hỗ trợ biên tập viên đăng bài, gắn danh mục, ảnh đại diện và kiểm soát thời điểm xuất bản nhanh chóng.</p>"),
            CreatePage("lien-he", "Liên hệ", "Liên hệ", "page", 10, true, "Liên hệ với doanh nghiệp", "Thông tin liên hệ, biểu mẫu tiếp nhận nhu cầu hợp tác và đầu mối phản hồi cho khách hàng, đối tác, ứng viên.", "<p>Bộ phận kinh doanh và dự án tiếp nhận thông tin từ website để chuyển xử lý theo đầu việc: báo giá, hồ sơ năng lực, tuyển dụng và hợp tác.</p>")
        };
    }

    public static IReadOnlyList<PostCategory> CreateCategories()
    {
        return new[]
        {
            new PostCategory { Name = "Tin nổi bật", Slug = "tin-noi-bat", Description = "Tin hoạt động, ký kết, khởi công và cập nhật nổi bật.", PostType = PostType.News, DisplayOrder = 1, IsVisible = true },
            new PostCategory { Name = "Thông báo", Slug = "thong-bao", Description = "Thông báo doanh nghiệp, tiến độ và mời thầu.", PostType = PostType.Announcement, DisplayOrder = 2, IsVisible = true },
            new PostCategory { Name = "Tuyển dụng", Slug = "tuyen-dung", Description = "Thông tin tuyển dụng nhân sự kỹ thuật và quản lý công trường.", PostType = PostType.Recruitment, DisplayOrder = 3, IsVisible = true }
        };
    }

    public static IReadOnlyList<ProjectEntry> CreateProjects()
    {
        return new[]
        {
            new ProjectEntry
            {
                Title = "Nhà máy linh kiện điện tử KCN Hòa Khánh",
                Slug = "nha-may-linh-kien-dien-tu-kcn-hoa-khanh",
                Summary = "Tổng thầu thi công phần kết cấu, hoàn thiện và hạ tầng kỹ thuật cho nhà máy sản xuất linh kiện điện tử.",
                BodyHtml = "<p>Dự án triển khai theo mô hình tổng thầu, tập trung kiểm soát tiến độ xây dựng và phối hợp chặt với gói MEP của chủ đầu tư.</p>",
                ClientName = "Công ty TNHH Suntech Components",
                Location = "Đà Nẵng",
                Scale = "24.000 m2 sàn",
                FeaturedImageUrl = "/images/project-factory.svg",
                IsFeatured = true,
                Status = ProjectStatus.Ongoing,
                DisplayOrder = 1,
                StartedOn = new DateOnly(2025, 10, 1)
            },
            new ProjectEntry
            {
                Title = "Trung tâm logistics miền Trung",
                Slug = "trung-tam-logistics-mien-trung",
                Summary = "Thi công hạ tầng kỹ thuật, kho hàng và khu điều phối xe container cho trung tâm logistics quy mô lớn.",
                BodyHtml = "<p>Công trình yêu cầu quản trị an toàn cao, điều phối nhiều mũi thi công và hoàn thiện hệ thống nền kho chịu tải trọng lớn.</p>",
                ClientName = "Công ty Cổ phần Logistics Duyên Hải",
                Location = "Quảng Nam",
                Scale = "12 ha",
                FeaturedImageUrl = "/images/project-logistics.svg",
                IsFeatured = true,
                Status = ProjectStatus.Ongoing,
                DisplayOrder = 2,
                StartedOn = new DateOnly(2025, 8, 15)
            },
            new ProjectEntry
            {
                Title = "Khối lớp học và nhà hiệu bộ Trường Đông Á",
                Slug = "khoi-lop-hoc-va-nha-hieu-bo-truong-dong-a",
                Summary = "Thi công kết cấu và hoàn thiện khối trường học đạt chuẩn vận hành an toàn, bền vững.",
                BodyHtml = "<p>Dự án đã hoàn thành đúng tiến độ, đáp ứng yêu cầu bàn giao trước năm học mới và được đánh giá cao về chất lượng hoàn thiện.</p>",
                ClientName = "Trường Liên cấp Đông Á",
                Location = "Huế",
                Scale = "8.500 m2 sàn",
                FeaturedImageUrl = "/images/project-campus.svg",
                IsFeatured = true,
                Status = ProjectStatus.Completed,
                DisplayOrder = 3,
                StartedOn = new DateOnly(2024, 2, 12),
                CompletedOn = new DateOnly(2024, 12, 20)
            },
            new ProjectEntry
            {
                Title = "Tòa nhà văn phòng và thương mại Blue Harbor",
                Slug = "toa-nha-van-phong-va-thuong-mai-blue-harbor",
                Summary = "Thi công phần thân, facade và hoàn thiện kiến trúc cho tổ hợp văn phòng - dịch vụ.",
                BodyHtml = "<p>Công trình hoàn thành với các yêu cầu khắt khe về chất lượng mặt dựng, tiến độ bàn giao và chuẩn hoàn thiện khu vực công cộng.</p>",
                ClientName = "Blue Harbor Development",
                Location = "Đà Nẵng",
                Scale = "18 tầng nổi",
                FeaturedImageUrl = "/images/project-office.svg",
                IsFeatured = false,
                Status = ProjectStatus.Completed,
                DisplayOrder = 4,
                StartedOn = new DateOnly(2023, 6, 1),
                CompletedOn = new DateOnly(2024, 8, 10)
            }
        };
    }

    public static IReadOnlyList<ContentPost> CreatePosts(IReadOnlyDictionary<string, int> categoryIds)
    {
        return new[]
        {
            new ContentPost
            {
                Title = "Việt Thành Công khởi công gói thầu kết cấu nhà máy điện tử",
                Slug = "viet-thanh-cong-khoi-cong-goi-thau-ket-cau-nha-may-dien-tu",
                Summary = "Dự án đánh dấu bước phát triển tiếp theo của doanh nghiệp trong phân khúc xây dựng công nghiệp cho khách hàng FDI.",
                BodyHtml = "<p>Lễ khởi công diễn ra với cam kết rõ ràng về tiến độ, an toàn và chất lượng thi công trong toàn bộ giai đoạn xây dựng.</p>",
                MetaDescription = "Tin nổi bật về lễ khởi công nhà máy điện tử.",
                FeaturedImageUrl = "/images/project-factory.svg",
                PostType = PostType.News,
                CategoryId = categoryIds["tin-noi-bat"],
                IsFeatured = true,
                PublishedAtUtc = new DateTime(2026, 3, 5, 8, 0, 0, DateTimeKind.Utc)
            },
            new ContentPost
            {
                Title = "Thông báo mời hợp tác cung cấp vật tư hoàn thiện",
                Slug = "thong-bao-moi-hop-tac-cung-cap-vat-tu-hoan-thien",
                Summary = "Thông báo tiếp nhận hồ sơ năng lực từ các nhà cung cấp vật tư phục vụ chuỗi dự án năm 2026.",
                BodyHtml = "<p>Doanh nghiệp ưu tiên các đơn vị có năng lực giao hàng ổn định, chứng từ chất lượng đầy đủ và kinh nghiệm làm việc với công trình công nghiệp.</p>",
                MetaDescription = "Thông báo mời hợp tác vật tư hoàn thiện.",
                FeaturedImageUrl = "/images/project-office.svg",
                PostType = PostType.Announcement,
                CategoryId = categoryIds["thong-bao"],
                PublishedAtUtc = new DateTime(2026, 3, 12, 3, 0, 0, DateTimeKind.Utc)
            },
            new ContentPost
            {
                Title = "Tuyển Chỉ huy trưởng công trình dân dụng",
                Slug = "tuyen-chi-huy-truong-cong-trinh-dan-dung",
                Summary = "Tuyển dụng nhân sự điều hành hiện trường cho các dự án dân dụng và thương mại tại miền Trung.",
                BodyHtml = "<p>Ứng viên cần có kinh nghiệm điều hành công trường, kiểm soát an toàn và phối hợp hiệu quả với chủ đầu tư, tư vấn giám sát và đội thi công.</p>",
                MetaDescription = "Thông tin tuyển Chỉ huy trưởng công trình.",
                FeaturedImageUrl = "/images/project-campus.svg",
                PostType = PostType.Recruitment,
                CategoryId = categoryIds["tuyen-dung"],
                IsFeatured = true,
                JobLocation = "Đà Nẵng, Quảng Nam",
                ExperienceRequirement = "Tối thiểu 5 năm",
                SalaryRange = "25 - 35 triệu đồng",
                ApplicationDeadline = new DateOnly(2026, 4, 30),
                PublishedAtUtc = new DateTime(2026, 3, 18, 2, 0, 0, DateTimeKind.Utc)
            },
            new ContentPost
            {
                Title = "Tuyển Kỹ sư QS hiện trường",
                Slug = "tuyen-ky-su-qs-hien-truong",
                Summary = "Bổ sung nhân sự QS để hỗ trợ kiểm soát khối lượng, hồ sơ thanh quyết toán và phối hợp nhà thầu phụ.",
                BodyHtml = "<p>Ưu tiên ứng viên có kinh nghiệm thi công nhà xưởng, nắm chắc bóc tách khối lượng và hồ sơ nghiệm thu hiện trường.</p>",
                MetaDescription = "Thông tin tuyển Kỹ sư QS hiện trường.",
                FeaturedImageUrl = "/images/project-logistics.svg",
                PostType = PostType.Recruitment,
                CategoryId = categoryIds["tuyen-dung"],
                JobLocation = "Đà Nẵng",
                ExperienceRequirement = "Từ 2 năm",
                SalaryRange = "14 - 20 triệu đồng",
                ApplicationDeadline = new DateOnly(2026, 4, 20),
                PublishedAtUtc = new DateTime(2026, 3, 20, 2, 0, 0, DateTimeKind.Utc)
            }
        };
    }

    private static CmsPage CreatePage(string slug, string title, string navigationTitle, string templateKey, int navigationOrder, bool showInNavigation, string heroTitle, string heroSummary, string bodyHtml)
    {
        return new CmsPage
        {
            Slug = slug,
            Title = title,
            NavigationTitle = navigationTitle,
            TemplateKey = templateKey,
            NavigationOrder = navigationOrder,
            ShowInNavigation = showInNavigation,
            HeroTitle = heroTitle,
            HeroSummary = heroSummary,
            Summary = heroSummary,
            MetaDescription = heroSummary,
            BodyHtml = bodyHtml,
            IsPublished = true
        };
    }
}
