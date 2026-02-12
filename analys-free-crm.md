# Analys: Free-CRM (go2ismail/Free-CRM)

## Sammanfattning

Free-CRM är ett CRM-system med öppen källkod byggt på ASP.NET Core 9.0. Projektet har
en funktionsrik frontend men lider av allvarliga brister vad gäller mognad, underhåll
och licensvillkor. **Rekommendationen är att inte använda detta projekt i produktion.**

---

## Teknikstack

| Komponent | Teknik |
|---|---|
| Backend | ASP.NET Core 9.0 (Headless API) |
| Frontend | Razor Pages + Vue.js (utan byggsystem) |
| Databas | SQL Server + Entity Framework Core |
| Arkitektur | Clean Architecture, CQRS, MediatR |
| UI-komponenter | Syncfusion Community Edition, AdminLTE |
| Övrigt | AutoMapper, FluentValidation, Serilog, JWT-auth |

## Funktioner

- Dashboard med kampanjkostnad, intäktsmål och säljtratt (7 steg)
- Kampanjhantering med automatisk numrering
- Budget- och utgiftshantering kopplad till kampanjer
- Leadhantering med BANT-poängsättning
- Aktivitetsloggning (e-post, telefon, möten, sociala medier)
- Sälj- och inköpsorder
- Teamhantering och tilldelning

## Projektmognad och aktivitet

| Mätvärde | Värde | Bedömning |
|---|---|---|
| Stjärnor | 227 | Lågt för ett CRM-projekt |
| Forks | 63 | Begränsat community |
| Bidragsgivare | 1 (enda utvecklare) | **Hög risk** |
| Commits | ~12 totalt | Mycket lågt |
| Senaste commit | Sep 2025 (innehåll: ".") | Minimal aktivitet |
| Öppna issues | 1 (obesvarad) | Inget underhåll |
| Stängda issues | 0 | Ingen bugghantering |
| Pull requests | Inga synliga | Inget community-bidrag |

### Commithistorik

- **Feb 2025**: Initialt uppsättning (9 commits, mestadels README/konfiguration)
- **Sep 2025**: 1 commit med meddelandet "." (ingen meningsfull ändring)
- **7 månaders gap** mellan aktivitetsperioderna

## Licensanalys (KRITISKT)

### CC BY 4.0 - Ovanligt val för mjukvara
Projektet använder **Creative Commons Attribution 4.0**, vilket är ovanligt och
problematiskt för programvara. CC-licenser rekommenderas inte för mjukvara av
Creative Commons själva. Standardval som MIT, Apache 2.0 eller GPL hade varit
mer lämpliga.

### Obligatorisk footer-länk
Licensen kräver att "footer link" behålls i alla instanser - detta är en
marknadsföringsmekanism, inte ett typiskt open source-krav.

### Syncfusion Community Edition - Begränsning
Syncfusion Community Edition är **gratis enbart för enskilda utvecklare och
småföretag**. Större organisationer måste köpa en kommersiell licens, vilket kan
kosta betydande summor.

## Risker

### Höga risker
1. **Ensam utvecklare** - Om go2ismail slutar underhålla projektet finns ingen som tar över
2. **Inaktivt projekt** - Praktiskt taget dött sedan feb 2025
3. **Syncfusion-licens** - Kan medföra oväntade kostnader vid skalning
4. **CC BY 4.0 för mjukvara** - Juridiskt otydligt jämfört med mjukvarulicenser
5. **Ingen testsvit synlig** - Ingen garanti för kodkvalitet

### Medelrisker
6. **SQL Server-beroende** - Kräver SQL Server (kostnad eller Linux-kompabilitet)
7. **Monolitisk arkitektur** - Medvetet val, men begränsar skalbarhet
8. **Ingen CI/CD** - Inga automatiserade tester eller byggen synliga
9. **Demo-lösenord "123456"** - Indikerar bristande säkerhetstänk

### Låga risker
10. **Teknikstack** - ASP.NET Core 9.0 är modernt och välstött av Microsoft

## Jämförelse med alternativ

| Projekt | Stjärnor | Licens | Aktivitet | Mognad |
|---|---|---|---|---|
| **Free-CRM** | 227 | CC BY 4.0 | Inaktivt | Låg |
| **SuiteCRM** | ~4k | AGPL-3.0 | Aktivt | Hög |
| **EspoCRM** | ~1.5k | AGPL-3.0 | Aktivt | Hög |
| **Twenty** | ~25k | AGPL-3.0 | Mycket aktivt | Medel |
| **Monica** | ~21k | AGPL-3.0 | Aktivt | Hög |

## Slutsats och rekommendation

### Använd INTE detta projekt om:
- Du behöver ett CRM i produktion
- Du behöver långsiktigt underhåll och säkerhetsuppdateringar
- Din organisation är större än ett småföretag (Syncfusion-licens)
- Du vill ha ett aktivt community

### Kan vara användbart som:
- **Studiematerial** - Clean Architecture med CQRS i ASP.NET Core
- **Prototyp-inspiration** - Bra funktionsrikt UI att lära av
- **Startpunkt för egen utveckling** - Om du planerar att underhålla och vidareutveckla helt själv

### Rekommenderade alternativ
1. **Twenty** (twenty.com) - Modernt, aktivt utvecklat, öppen källkod CRM
2. **SuiteCRM** - Mogen PHP-baserad CRM med stort community
3. **EspoCRM** - Lättviktigt och flexibelt CRM
