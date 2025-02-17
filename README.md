# 📖 Backend (.NET) Assessment

## 🔧 Kurulum & Çalıştırma

### 1️⃣ **Projeyi Klonlayın**
```bash
git clone https://github.com/cmktass/Setur-Assessment.git
cd Setur-Assessment
```

### 2️⃣ **Docker ile Rabbitmq ve Postgresql'i Başlatın**
```bash
 docker-compose -f docker-compose.yaml up
```
> 🛠 Bu komut, **PostgreSQL, RabbitMQ'yu** çalıştıracaktır.
> Daha sonra visual studio üzerinden solution'u açıp Multiplestartup projectten ContractService.Api ve ReportService.Api Start seçilerek proje ayağa kaldırılabilir.
> Code first ile db yapısı oluşturulmuştur. İnitial migration oluşturulmuştur. Auto migration vardır. Contact Service'e proje ayağa kalktığında dummy data yükleyecektir.

> ⚠ **NOT:** Localinizde PostgreSQL ve rabbitmq yüklüyse OS'nizin servislerinden kapatınız. Docker'da da çalışacağı için port çakışması yaşayacaktır.

> ❗ **NOT:** Farklı bir container üzerinde de PostgreSQL veya RabbitMQ ayaktaysa ve host portunuzla çakışıyorsa hata alabilirsiniz.



## 📌 Proje Kapsamı
Bu proje, **.NET 8** ve modern yazılım geliştirme prensipleri ile oluşturulmuş **telefon rehberi uygulamasıdır**.
Mikroservis mimarisine dayalı olarak inşa edilmiş olup, **CQRS, Clean Architecture, Vertical Slice ve DDD** prensiplerine uygun geliştirilmiştir.
- **Servicelerde kullanılmak üzere base libary eklenmiştir. Bu base libary'de **
-ValidationBehavior,
-BaseExceptionTypes,
-BaseDbContext,
-RabbitMQConfiguration,
-CorrelationIdMiddleware
işlemleri eklenmiştir.
Projeye eklenilmesi planlananlar
retry mekanizmaları,
logging,
unit, integration tests.

---

## 🚀 Kullanılan Teknolojiler ve Kütüphaneler

### 🔹 **Backend**
- **.NET 8**
- **EntityFrameworkCore & EntityFrameworkCore.Tools** (Veri erişimi)
- **Carter** (Minimal API)
- **Mapster** (DTO Mapping)
- **MassTransit & RabbitMQ** (Event-Driven Architecture)
- **MediatR** (CQRS Pattern)
- **FluentValidation**

### 🔹 **Diğer Araçlar**
- **RabbitMQ** (Mesaj kuyruğu)
- **Docker & Docker-Compose** (Konteynerleştirme)
- **PostgreSQL** (Veritabanı yönetimi)

---

## 📖 Senaryo
Projede, **birbirleriyle haberleşen en az iki mikroservis** içeren bir sistem tasarlanarak **basit bir telefon rehberi uygulaması** geliştirilmiştir.

