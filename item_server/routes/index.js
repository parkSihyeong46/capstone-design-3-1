var express = require('express');
var router = express.Router();

let requestCounter = 1;

/* GET home page. */
router.get('/', function(req, res, next) {
  res.send("ok")
});
/* GET Item priece list */
router.get('/price', function(req, res, next) {
  if (requestCounter) {
    res.json({"parsnip": 300, "parsnipSeed": 10, "cauliflower": 100,"cauliflowerSeed": 5,"strawberry": 500,"strawberrySeed": 50,"seeds": 20,"icecream": 150,"survivalHamburger": 80,"friedEgg": 50,"cheeseCauliflower": 30})
    requestCounter--;
  }
  else{
    res.json({"parsnip": 301, "parsnipSeed": 11, "cauliflower": 101,"cauliflowerSeed": 6,"strawberry": 501,"strawberrySeed": 51,"seeds": 21,"icecream": 151,"survivalHamburger": 81,"friedEgg": 51,"cheeseCauliflower": 31})
    requestCounter++;
  }

});


/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Express' });
});

module.exports = router;
