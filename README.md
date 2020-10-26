# Pass

Pass on Windowsilla toimivan salasanojenhallintasovelluksen Android-versio. Ohjelmalla voidaan tässä vaiheessa ainoastaan selata palveluita ja niiden salasanoja, sekä kopioida kirjautumistietoja leikepöydälle, mutta tietojen muokkaus tai lisäys ei vielä onnistu.

Ensimmäisenä kannattaa lukea artikkeli ohjelman [Windows-versiosta](https://github.com/arikankainen/pass-windows), sillä ilman sitä ei Android-versiolla ole mitään käyttöä.

Ohjelma lukee Windows-versiolla tallennetun salatun listan käyttäjän omalta FTP-palvelimelta. Ohjelman asetuksissa (jonne pääsee täppäämällä pääikkunan kolmea pistettä), määritellään FTP-palvelimen osoite, sekä polku `pass.dat`-tiedostoon (`ftp://ftp.fakeserver.fi/datastorage/pass.dat`). Lisäksi määritellään FTP-palvelimen kirjautumistiedot, sekä salasana, jolla salattu lista aukeaa.

<img src="/docs/both.png" width="700">

## Käyttö

Ohjelmaa avattaessa se käy lukemassa `pass.dat`-tiedoston käyttäjän määrittelemältä FTP-palvelimelta. Tiedostoa ei kannata tallentaa julkisesti näkyvillä olevaan kansioon palvelimella, vaikka se onkin salattu. Ohjelma purkaa salauksen asetuksissa määritellyllä salasanalla, ja näyttää sen sisällön listana, jossa näkyy palvelun nimi ja verkko-osoite. Palvelua klikkaamalla, avautuu `View credentials`-näkymä, jossa eri tietoja voidaan kopioida leikepöydälle niitä täppäämällä. `Comments`-kenttää klikkaamalla siitä voidaan valita haluttua tekstiä, joka on mahdollista kopioida leikepöydälle. Päänäkymään palaaminen tyhjentää leikepöydän.

## Lataus

En ota mitään vastuuta ohjelman mahdollisesti aiheuttamista vahingoista; kukin käyttää ohjelmaa omalla vastuullaan. Vaatii Androidin. Ohjelma on itsellä täydessä käytössä, mutta se on käytännössä kuitenkin vielä pahasti keskeneräinen.

**_Ohjelmasta ei ole tällä hetkellä ladattavana käännettyä versiota, ainoastaan lähdekoodi._**
