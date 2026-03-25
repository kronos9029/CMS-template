USE viet_construction_cms;
SET NAMES utf8mb4;

UPDATE Posts SET
    Title = 'Tuyển Kỹ sư QA/QC công trình',
    Summary = 'Bổ sung kỹ sư QA/QC phụ trách hồ sơ chất lượng, nghiệm thu và kiểm soát vật tư đầu vào.',
    BodyHtml = '<p>Ưu tiên ứng viên có kinh nghiệm triển khai QA/QC cho công trình dân dụng hoặc công nghiệp, nắm chắc quy trình nghiệm thu và hoàn công.</p>',
    MetaDescription = 'Thông tin tuyển Kỹ sư QA/QC công trình.',
    JobLocation = 'Đà Nẵng',
    ExperienceRequirement = 'Từ 3 năm',
    SalaryRange = '15 - 22 triệu đồng'
WHERE Slug = 'tuyen-ky-su-qa-qc-cong-trinh';

UPDATE Posts SET
    Title = 'Tuyển Kỹ sư kế hoạch tiến độ',
    Summary = 'Tuyển nhân sự theo dõi tiến độ, cập nhật KPI thi công và phối hợp báo cáo cho ban điều hành.',
    BodyHtml = '<p>Ứng viên có khả năng lập và theo dõi baseline tiến độ, tổng hợp số liệu hiện trường và cảnh báo chướng ngại theo tuần.</p>',
    MetaDescription = 'Thông tin tuyển Kỹ sư kế hoạch tiến độ.',
    JobLocation = 'Đà Nẵng, Quảng Ngãi',
    ExperienceRequirement = 'Từ 3 năm',
    SalaryRange = '16 - 24 triệu đồng'
WHERE Slug = 'tuyen-ky-su-ke-hoach-tien-do';

UPDATE Posts SET
    Title = 'Tuyển Kỹ sư MEP hiện trường',
    Summary = 'Bổ sung nhân sự quản lý thi công hệ cơ điện, phối hợp shopdrawing và nghiệm thu hiện trường.',
    BodyHtml = '<p>Yêu cầu có kinh nghiệm làm việc với hệ HVAC, PCCC, cấp thoát nước hoặc điện động lực tại công trình nhà xưởng, tòa nhà.</p>',
    MetaDescription = 'Thông tin tuyển Kỹ sư MEP hiện trường.',
    JobLocation = 'Bình Dương',
    ExperienceRequirement = 'Từ 3 năm',
    SalaryRange = '18 - 28 triệu đồng'
WHERE Slug = 'tuyen-ky-su-mep-hien-truong';

UPDATE Posts SET
    Title = 'Tuyển Kỹ sư hiện trường cấp thoát nước',
    Summary = 'Tuyển kỹ sư triển khai hạng mục hạ tầng kỹ thuật, mạng lưới cấp thoát nước và hố ga ngoài nhà.',
    BodyHtml = '<p>Ứng viên có kinh nghiệm thi công hạ tầng khu công nghiệp hoặc khu đô thị, có khả năng bám sát hiện trường và xử lý phát sinh nhanh.</p>',
    MetaDescription = 'Thông tin tuyển Kỹ sư hiện trường cấp thoát nước.',
    JobLocation = 'Quảng Nam',
    ExperienceRequirement = 'Từ 2 năm',
    SalaryRange = '14 - 21 triệu đồng'
WHERE Slug = 'tuyen-ky-su-hien-truong-cap-thoat-nuoc';

UPDATE Posts SET
    Title = 'Tuyển Cán bộ an toàn lao động',
    Summary = 'Tuyển nhân sự HSE phụ trách kiểm tra an toàn, huấn luyện đầu ca và giám sát tuân thủ trên công trường.',
    BodyHtml = '<p>Yêu cầu có chứng chỉ an toàn lao động phù hợp, kinh nghiệm triển khai toolbox meeting, permit to work và báo cáo safety walkdown.</p>',
    MetaDescription = 'Thông tin tuyển Cán bộ an toàn lao động.',
    JobLocation = 'Đà Nẵng, Huế',
    ExperienceRequirement = 'Từ 2 năm',
    SalaryRange = '13 - 19 triệu đồng'
WHERE Slug = 'tuyen-can-bo-an-toan-lao-dong';

UPDATE Posts SET
    Title = 'Tuyển Kế toán công trình',
    Summary = 'Bổ sung nhân sự theo dõi tạm ứng, hồ sơ khái toán, thanh toán nhà thầu phụ và chi phí hiện trường.',
    BodyHtml = '<p>Ưu tiên ứng viên đã làm việc tại công ty xây dựng, hiểu quy trình thanh toán khối lượng, tạm ứng và đối chiếu chi phí theo công trình.</p>',
    MetaDescription = 'Thông tin tuyển Kế toán công trình.',
    JobLocation = 'Đà Nẵng',
    ExperienceRequirement = 'Từ 2 năm',
    SalaryRange = '12 - 17 triệu đồng'
WHERE Slug = 'tuyen-ke-toan-cong-trinh';

UPDATE Posts SET
    Title = 'Tuyển Kỹ sư shopdrawing',
    Summary = 'Tuyển nhân sự triển khai shopdrawing kết cấu, kiến trúc và phối hợp RFI/RFC trong quá trình thi công.',
    BodyHtml = '<p>Yêu cầu sử dụng tốt AutoCAD, đọc hiểu bản vẽ kết cấu và có kinh nghiệm phối hợp bộ môn hiện trường.</p>',
    MetaDescription = 'Thông tin tuyển Kỹ sư shopdrawing.',
    JobLocation = 'Đà Nẵng',
    ExperienceRequirement = 'Từ 2 năm',
    SalaryRange = '14 - 20 triệu đồng'
WHERE Slug = 'tuyen-ky-su-shopdrawing';

UPDATE Posts SET
    Title = 'Tuyển Kỹ sư trắc đạc công trình',
    Summary = 'Bổ sung nhân sự phụ trách công tác trắc đạc, chuyển trục, nghiệm thu cao độ, tim trục và hoàn công.',
    BodyHtml = '<p>Ưu tiên ứng viên có kinh nghiệm sử dụng máy toàn đạc, máy thủy bình và phối hợp trắc đạc cho công trình cao tầng hoặc hạ tầng.</p>',
    MetaDescription = 'Thông tin tuyển Kỹ sư trắc đạc công trình.',
    JobLocation = 'Quảng Ngãi',
    ExperienceRequirement = 'Từ 2 năm',
    SalaryRange = '13 - 18 triệu đồng'
WHERE Slug = 'tuyen-ky-su-trac-dac-cong-trinh';

UPDATE Posts SET
    Title = 'Tuyển Chuyên viên hồ sơ thanh quyết toán',
    Summary = 'Tuyển nhân sự phụ trách hồ sơ thanh quyết toán, đối chiếu khối lượng và tổng hợp báo cáo khách hàng.',
    BodyHtml = '<p>Ứng viên có kinh nghiệm với hồ sơ nghiệm thu, thanh quyết toán công trình và khả năng phối hợp QS, chủ đầu tư, tư vấn giám sát.</p>',
    MetaDescription = 'Thông tin tuyển Chuyên viên hồ sơ thanh quyết toán.',
    JobLocation = 'Đà Nẵng',
    ExperienceRequirement = 'Từ 3 năm',
    SalaryRange = '16 - 23 triệu đồng'
WHERE Slug = 'tuyen-chuyen-vien-ho-so-thanh-quyet-toan';

UPDATE Posts SET
    Title = 'Tuyển Kỹ sư hiện trường công nghiệp',
    Summary = 'Tuyển nhân sự phụ trách thi công hiện trường cho các công trình nhà xưởng, kho và hạ tầng phụ trợ.',
    BodyHtml = '<p>Yêu cầu có kinh nghiệm quản lý đội thi công, bám sát tiến độ, triển khai biện pháp thi công và phối hợp nhà thầu phụ.</p>',
    MetaDescription = 'Thông tin tuyển Kỹ sư hiện trường công nghiệp.',
    JobLocation = 'Long An, Bình Dương',
    ExperienceRequirement = 'Từ 4 năm',
    SalaryRange = '20 - 30 triệu đồng'
WHERE Slug = 'tuyen-ky-su-hien-truong-cong-nghiep';
