# Docker Deploy

Stack deploy cho monolith `.NET` va `MySQL` duoc dong goi bang `docker compose`.

## Thanh phan

- `vietconstruction-web`: ASP.NET Core monolith chay public site, CMS admin va API.
- `vietconstruction-db`: MySQL 8.4 cho du lieu CMS.

## Persistent data

- MySQL data volume: `vietconstruction_mysql_data`
- Data Protection keys volume: `vietconstruction_dataprotection_keys`

Hai volume nay la named volume, nen `docker compose down` se khong xoa data.
Data chi bi xoa neu dung `docker compose down -v` hoac xoa volume bang tay.

## File can dung

- Compose file: `docker-compose.yml`
- Environment file: `.env.docker`
- Sample env: `.env.docker.example`

## Chay stack

```powershell
docker compose --env-file .env.docker up -d --build
```

Website public va CMS mac dinh:

- Public site: `http://localhost:8080`
- CMS login: `http://localhost:8080/login`
- CMS admin: `http://localhost:8080/admin`

MySQL duoc expose ra host tai:

- `127.0.0.1:3307`

## Lenh huu ich

Xem config sau khi render env:

```powershell
docker compose --env-file .env.docker config
```

Xem log:

```powershell
docker compose --env-file .env.docker logs -f
```

Dung stack nhung giu du lieu:

```powershell
docker compose --env-file .env.docker down
```

Dung stack va xoa ca volume:

```powershell
docker compose --env-file .env.docker down -v
```

## Luu y

- Doi password trong `.env.docker` truoc khi deploy that.
- App se retry ket noi DB trong giai doan startup. Neu DB van khong san sang sau so lan retry, container web se fail de Docker restart dung cach.
- Data Protection keys duoc mount rieng de cookie dang nhap khong bi invalid sau khi recreate container web.
