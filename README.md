# Mars Weather

Tiimi: Heta Björklund, Joni Jaakkola, Christian Lindell, Dmitry Sinyavskiy ja Irina Tregub.

## Johdanto

Mars Weather on sääsovellus, joka näyttää Marsin sään. Käyttäjä voi katsoa tämänhetkistä säätä, viimeisen viikon säätä, tai hakea säätiedot tietyltä päivältä. Sovellus näyttää myös tilastotietoa sään vaihtelusta. Sovellus hakee säätiedot NASAn avoimesta ["InSight: Mars Weather Service API"-rajapinnasta](api.nasa.gov/insight_weather). Lisää tietoa InSight-missiosta löytyy [täältä](https://mars.nasa.gov/insight/weather/) ja NASAn rajapinta on dokumentoitu [täällä](https://api.nasa.gov/assets/insight/InSight%20Weather%20API%20Documentation.pdf).

## Järjestelmän määrittely

Alla kuvattuna järjestelmän tarpeet tarkemmin käyttäjätarinoiden ja niistä johdettujen vaatimusten kautta.

### Käyttäjätarinat

KT1 Tähtitieteen harrastajana haluan nähdä, millainen sää Marsissa on nyt, jotta voin verrata säätilaa kaukoputkella tekemiini näköhavaintoihin. 

KT2 Maantieteen opettajana haluan helppokäyttöisen työkalun, jolla selittää ja opettaa oppilaille sään ja ilmaston toimintaa. 

KT3 Tähtitieteen opiskelijana haluan yhdestä palvelusta helposti minua kiinnostavia avaruussään tietoja, pysyäkseni tietoisena ”paikallisista” astronomian tapahtumista. 

KT4 Viikonlopputieteilijänä haluan keskitetysti tietoa maan, paikallisen avaruussään ja Marsin säästä, jotta voisin tutkiskella korrelaatiota näiden välillä. 

KT5 Teknisesti taitamattomana tiedeharrastajana haluaisin selkeästi yhdestä paikasta kaikki Nasan API-tiedot selkeästi esiteltynä harrastuksiani varten. 

KT6 Roolipelikampanjan vetäjänä haluaisin pelikampanjani materiaaliksi esityksiä ja karttoja aurinkokunnan säästä ja kiertävistä asteroideista helposti yhdestä paikasta. 

KT7 Tähtitieteen harrastajana haluan nähdä, millainen sää Marsissa on ollut viimeisen viikon aikana, jotta voin verrata säätilaa aikaisempiin kaukoputkella tekemiini näköhavaintoihin. 

KT8 Tulevana avaruusmatkailijana haluan saada lämpötilatiedot eri lämpötilayksiköissä (Fahrenheit/Celsius), mutta en halua, että molemmat näkyisivät samalla, jotta ei tulisi sekaannuksia.  

KT9 Tähtitieteen opiskelijana haluan saada mahdollisia tilastotietoja (esim. Päivän/viikon/vuoden matalin ja korkein lämpötila, tuulen keskinopeus).

KT10 Avaruusmatkailijana haluaisin tietää tulevaa säätä, mutta jos sovellus ei tee sääennustuksia, minua auttaisi, jos olisi mahdollista saada tiedot valitulle päivälle, esimerkiksi sää X päivänä vuosi sitten.

### Käyttäjätarinoista johdetut vaatimukset

Yleisellä tasolla esitellyt vaatimukset, jotka nousevat käyttäjätarinoista. Vaatimuksen perässä on suluissa merkitty käyttäjätarinat, joista vaatimus tulee. Vaatimuksista osa on toteutettu, ja osan kohdalla on todettu, ettei niiden toteuttaminen ole tämän projektin puitteissa prioriteetti ja nämä vaatimukset on poistettu työjonosta. Niihin on kuitenkin mahdollista palata tulevaisuudessa, jos projektia halutaan jatkokehittää.

#### Toteutetut vaatimukset

* Sovellus näyttää tämänhetkisen säätilan Marsissa (KT1).
* Käyttäjä voi valita, mitä tietoja näkee sovelluksessa (KT2).
* Käyttöliittymä on intuitiivinen (KT2).
* Backend hoitaa tietojen haun, lajittelun/suodatuksen/koosteiden teon ja kertoo frontendille vain sen, mitä frontend tarvitsee näyttääkseen näkymän oikein. Frontend ei käsittele tietoja itse (KT4, KT5).
* Palvelu esittää tiedon helposti jatkokäytettävässä/jatkokehitettävässä/uudelleenkäytettävässä muodossa (KT6).
* Sovellus näyttää Marsin sään viimeisen viikon ajalta (KT7).
* Mahdollisuus tehdä tiedoista tilastoja tai keskiarvoja pidemmältä aikaväliltä (KT9).
* Mennyttä säätä voi tarkastella päivän tarkkuudella (KT10).

#### Työjonosta poistetut vaatimukset

* Backend hakee tietoa monesta eri APIsta ja yhdistää tiedot käyttäjälle (KT3, KT4, KT5).
* Sovellus näyttää samanaikaista tietoa monesta eri paikasta, jolloin tietoja pystyy korreloimaan keskenään (KT4).
* Tulevaa säätä voi ennustaa (KT10).

## Luokkakaavio

![Luokkakaavio](./assets/classdiagram.png)

## Tekninen toteutus

### Back end

Back end on toteutettu [ASP.NET Coren web-sovelluksena](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio-code).

Back endin GitHub-repositorio löytyy osoitteesta https://github.com/Mars-Weather/marsweatherapi

Syy siihen, että front end ei hae dataa suoraan Nasan rajapinnasta, vaan data kierrätetään oman back endin ja tietokannan kautta, on se, että vanhaa dataa voidaan tallentaa, näyttää historiatietoja (eikä ainoastaan viimeisintä 7 solia, jonka Nasan rajapinta palauttaa) ja luoda tilastoja sään vaihtelusta.

Koska NASAn rajapinnasta ei aina saa luotettavaa dataa [Marsin sääolosuhteiden takia](https://mars.nasa.gov/news/8858/insight-is-meeting-the-challenge-of-winter-on-dusty-mars/?site=insight), sovellus käyttää myös generoitua dataa, joka on saman muotoista kuin oikea, NASAn rajapinnasta saatava data, mutta joka ei perustu oikeisiin mittauksiin. Generoitu data on luotu [JSON Generatorilla](https://json-generator.com/).

[DbUpdateService](./Services/DbUpdateService.cs)-luokka on ajoitettu taustapalvelu, joka säännöllisin väliajoin tarkistaa, ovatko generoidun datan sisältämät Solit jo tietokannassa ja jos eivät ole, se lisää generoidun datan tietokantaan. Huomaa, että palvelu tarkistaa isoimman tietokannasta löytyvän Solnumberin ja vertaa sitä generoidun datan Solnumbereihin. Se olettaa, että Solien numerot kasvavat ajan kuluessa eteenpäin, joten jos tietokantaan luodaan manuaalisesti tai Postmanilla Sol, jonka Solnumber on suurempi kuin generoidun datan isoin Solnumber, palvelu olettaa että myös kaikki sitä pienemmät Solnumberit ovat jo tietokannassa, eikä lisää generoitua dataa.

Projektissa on neljä haaraa: master, localdb, localdb-testing ja remotedb.

#### Master
- master-haara on kehityshaara.
- Tietokanta on InMemory-tietokanta. Tietokanta ei ole persistentti, vaan tiedot häviävät kun sovellus sammutetaan.
- Endpointit ovat muotoa localhost:[portti]/api/sol

#### Localdb
- localdb-haara on kehityshaara.
- Tietokanta on Microsoft SQL Server. Tietokanta on persistentti, mutta sinne pääsee käsiksi vain paikallisesti.
- Endpointit ovat muotoa localhost:[portti]/api/sol

#### Localdb-testing
- localdb-testing on testaushaara. Sen sisältö on testejä lukuunottamatta muuten identtinen localdb-haaran kanssa, mutta kansiorakenne on erilainen, koska testaus .NETin testaustyökalulla ja xUnitilla vaatii, että lähdekoodi ja testauskoodi ovat erillisissä kansioissa.
- Tietokanta on Microsoft SQL Server. Tietokanta on persistentti, mutta sinne pääsee käsiksi vain paikallisesti.
- Endpointit ovat muotoa localhost:[portti]/api/sol

#### Remotedb
- remotedb-haara on projektin tuotantohaara ja julkaistu versio.
- Tietokanta on MySQL. Tietokanta on persistentti ja sitä voi hallinnoida ja muokata Azuren dashboardin kautta (ei julkinen, vain tiimin jäsenten saatavilla).
- Tietokantaa voi tarkastella myös lähettämällä GET-pyyntöjä Postmanilla. Avoin pyyntökokoelma löytyy osoitteesta https://www.postman.com/hetabjorklund/workspace/mars-weather-public/
- Julkaistun sovelluksen osoite on https://marsweather.azurewebsites.net/
- Endpointit ovat muotoa https://marsweather.azurewebsites.net/api/sol. GET-pyynnöt ovat avoimia, kaikki muut pyynnöt vaativat API-avaimen pyyntöparametrina (vain tiimin jäsenten saatavilla).

### Front end

Front end on toteutettu React Appina. 

Front endin GitHub-repositorio löytyy osoitteesta https://github.com/Mars-Weather/marsweatherui

Julkaistun front endin osoite on https://weather-mars.herokuapp.com/

## Testaus

Sovelluksen testaukseen on käytetty .NETin omaa sisäänrakennettua testaustyökalua (*dotnet test*) ja [xUnit-testaustyökalua](https://xunit.net/). Seurasin [Microsofin ohjetta testien luomiseksi](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test). Yksikkötestit testaavat paikallista tietokantaa ja integraatiotestit Sol-luokan controlleria ja julkaistun sovelluksen palvelimella sijaitsevaa tietokantaa.

Testaaminen on toteutettu Mars Weather -projektiin omana haaranaan nimeltä [localdb-testing](https://github.com/Mars-Weather/marsweatherapi/tree/localdb-testing). Sen sisältö on testejä lukuunottamatta muuten identtinen paikallista tietokantaa käyttävän localdb-haaran kanssa, mutta kansiorakenne on erilainen, koska testaus .NETin testaustyökalulla ja xUnitilla vaatii, että lähdekoodi ja testauskoodi ovat erillisissä kansioissa.

Testien ajaminen onnistuu komennolla *dotnet test*. Erilliset testiprojektit yksikkö- ja integraatiotesteille mahdollistavat sen, että yksikkö- ja integraatiotestit voi ajaa erikseen. Siirtymällä testiprojektikansioon (*cd ./unittests* tai *cd ./integrationtests*), siitä kansiosta komennolla *dotnet test* ajetaan vain kyseisen kansion testit. Jos komennon kirjoittaa koko projektin juuressa, ajetaan yhdellä kertaa sekä yksikkö- että integraatiotestit.

### Yksikkötestit

Paikallista tietokantaa testaavat yksikkötestit ovat tiedostossa [DbTest.cs](https://github.com/Mars-Weather/marsweatherapi/blob/localdb-testing/unittests/DbTest.cs).
* DbConnectionExists tarkistaa, onnistuuko yhteyden avaaminen.
* DbIsRelational tarkistaa, että tietokanta on relaatiotietokanta.
* DbIsNotInMemory tarkistaa, että tietokanta ei ole InMemory-tietokanta.
* DbIsSqlServer tarkistaa, että tietokanta on SQLServer.

### Integraatiotestit

SolControlleria ja julkaistun sovelluksen palvelimella sijaitsevaa tietokantaa testaavat integraatiotestit ovat tiedostossa [SolController.cs](https://github.com/Mars-Weather/marsweatherapi/blob/localdb-testing/integrationtests/SolControllerTest.cs).
* DbIsNotEmpty tarkistaa, että tietokannassa on tietoja (tietokanta täytetään sovelluksen käynnistyessä, joten sen ei pitäisi olla tyhjä). Se lähettää GET-pyynnön julkaistun sovelluksen polkuun https://marsweather.azurewebsites.net/api/sol, jonka pitäisi palauttaa kaikki tietokannan Solit. Testi tarkistaa, että vastauksena saadun listan koko ei ole 0.
* LastSevenSolsContainsSeven lähettää GET-pyynnön julkaistun sovelluksen polkuun https://marsweather.azurewebsites.net/api/sol/solweek, jonka pitäisi palauttaa viimeisimmät seitsemän Solia. Testi tarkistaa, että vastauksena saadun listan koko on 7.

Testattaviksi kohteiksi valikoituivat paikallinen tietokanta (sen yhteyden toimiminen ja tietokannan ominaisuudet), yksi controlleri ja palvelimella sijaitseva tietokanta (vastaanottaako controlleri pyynnön ja palautuuko tietokannasta sitä, mitä oletetaan). Testaaminen on rajallista, koska vain yhtä controlleria testataan ja vain GET-pyynnöillä. Laajempi testaaminen vaatisi myös muiden controllerien testaamisen ja myös muilla kuin GET-pyynnöillä.