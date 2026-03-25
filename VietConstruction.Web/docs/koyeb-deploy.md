# Deploy Len Koyeb

Tai lieu nay dung cho monolith ASP.NET Core o repo nay khi deploy len Koyeb bang Docker service.

## Kien truc nen dung

Koyeb nen chay **web app .NET** thanh mot Service rieng. Phan MySQL co 2 lua chon:

### Lua chon A: MySQL ngoai Koyeb

Day la huong khuyen nghi cho production.

- Web app: Koyeb Docker service
- Database: PlanetScale, Aiven MySQL, Railway MySQL, hoac MySQL VPS/managed MySQL khac

Ly do:

- app nay can MySQL persistent
- Koyeb volume hien dang o public preview va docs khuyen backup can than
- van hanh database rieng ben ngoai de on dinh hon cho production

### Lua chon B: MySQL tren chinh Koyeb

Van lam duoc, nhung day la self-hosted MySQL chay nhu mot Service khac trong cung App.

- Tao mot MySQL service bang one-click MySQL app cua Koyeb
- Web app noi vao MySQL qua private service domain cua Koyeb
- Nen attach volume cho MySQL va co backup/snapshot

Can luu y:

- Koyeb volume hien la public preview
- Service mesh/private domain khong hoat dong tren free instance
- Neu dung free instance, ban se phai noi DB qua public TCP proxy thay vi private mesh

## File da san sang trong repo

- Docker image: [Dockerfile](/D:/aws/VietConstruction.Web/Dockerfile)
- Health endpoint: [Program.cs](/D:/aws/VietConstruction.Web/Program.cs)
- Env mau: [koyeb.env.example](/D:/aws/VietConstruction.Web/docs/koyeb.env.example)

## Chuan bi truoc khi deploy

1. Push code hien tai len GitHub.
2. Chot ban se dung `Lua chon A` hay `Lua chon B`.
3. Tao MySQL va lay connection string phu hop.
4. Tao password moi cho `Admin` va `Editor`.
5. Chon region Koyeb gan database nhat.

## Cach deploy tren Koyeb

1. Vao Koyeb control panel.
2. Tao `App` moi.
3. Chon `GitHub` hoac `Dockerfile builder` tu repo nay.
4. Chon repo `CMS-template` va branch ban muon deploy.
5. Root directory: `VietConstruction.Web`
6. Port expose: `8080`
7. Health check path: `/health`
8. Instance: bat dau voi 1 instance.

## Environment variables can set

Nhap cac bien moi truong tu file [koyeb.env.example](/D:/aws/VietConstruction.Web/docs/koyeb.env.example) vao Koyeb:

- `ASPNETCORE_ENVIRONMENT=Production`
- `ASPNETCORE_HTTP_PORTS=8080`
- `ConnectionStrings__MySql=...`
- `CmsSeed__Enabled=true`
- `CmsSeed__AdminEmail=...`
- `CmsSeed__AdminPassword=...`
- `CmsSeed__AdminFullName=...`
- `CmsSeed__EditorEmail=...`
- `CmsSeed__EditorPassword=...`
- `CmsSeed__EditorFullName=...`
- `DatabaseInitialization__MaxAttempts=20`
- `DatabaseInitialization__DelaySeconds=5`

## Data Protection keys

Neu khong mount persistent storage cho Data Protection keys, cookie login CMS co the bi vo hieu sau moi lan redeploy. Co 2 cach:

### Cach 1: Chap nhan dang nhap lai sau redeploy

Khong can set gi them.

### Cach 2: Mount Koyeb volume cho keys

Neu ban dung Koyeb volume:

- Mount volume vao: `/var/lib/vietconstruction/keys`
- Set env: `DataProtection__KeysPath=/var/lib/vietconstruction/keys`

Luu y:

- Koyeb volume hien la public preview.
- Volume chi phu hop service scale = 1.
- Nen backup neu dung volume cho du lieu quan trong.

## Seed tai khoan

Lan deploy dau:

- de `CmsSeed__Enabled=true`
- app se tao role `Admin`, `Editor` va tai khoan seed neu chua ton tai

Sau khi dang nhap va doi mat khau:

- doi `CmsSeed__Enabled=false`
- redeploy lai service

## Connection string mau

Vi du:

```text
Server=your-mysql-host;
Port=3306;
Database=viet_construction_cms;
User Id=vietconstruction_app;
Password=replace-me;
Character Set=utf8mb4;
SslMode=Required;
```

Neu provider cua ban khong bat buoc SSL, co the bo `SslMode=Required`, nhung production nen dung SSL.

### Neu dung MySQL service trong cung App tren Koyeb

Ban co the dung private service discovery cua Koyeb. Hay copy dung private domain ma Koyeb hien trong service overview cua MySQL service roi gan vao `Server=...`.

```text
Server=<private-mysql-domain-from-koyeb>;
Port=3306;
Database=viet_construction_cms;
User Id=vietconstruction_app;
Password=replace-me;
Character Set=utf8mb4;
```

Neu ban khong co private mesh, hay dung public TCP proxy endpoint do Koyeb cap cho MySQL service.

## Nhung diem can kiem tra sau deploy

1. Mo `/health` va xac nhan tra `200`.
2. Mo `/login` va dang nhap bang tai khoan seed.
3. Tao mot bai viet moi trong CMS.
4. Kiem tra giao dien public hien du lieu moi.
5. Sau khi on dinh, tat seed bang `CmsSeed__Enabled=false`.

## Nhung gi chua the lam tu may nay

May nay hien khong co `koyeb` CLI va khong co session dang nhap Koyeb, nen chua the tao service tren tai khoan cua ban truc tiep tu terminal nay.
