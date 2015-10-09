var sec = 0;
var timer;
var running = false;
var harmonyMode = Math.random() >= 0.5; //The mode not to start in :)

function pad(val) {
    return val > 9 ? val : "0" + val;
}



function startGame() {
    startbtn.style.visibility = "hidden";
    resetGame()
    sec = 0;
    running = true;
    document.getElementById("seconds").innerHTML = pad(0);
    document.getElementById("minutes").innerHTML = pad(0);
    
    timer = setInterval(function () {
        document.getElementById("seconds").innerHTML = pad(++sec % 60);
        document.getElementById("minutes").innerHTML = pad(parseInt(sec / 60, 10));
    }, 1000);
};

function stopGame() {
    clearInterval(timer);
    addSession(sec, tries, harmonyMode);
    running = false;
    console.log("Harmony: " + harmonyMode + "Tries: " + tries + ", time: " + sec );
    startbtn.style.visibility = "visible";
}

function cardClick(element, index)
{   
    if(running) {
        var tmp = element.getAttribute("class");

        if(tmp=="card"){ //Is the backside of the card visible?
            //set the src of the card to the image corresponding to this cards id.
            element.setAttribute("src", images[cards[index].identifier]);
            if(harmonyMode) {
                element.setAttribute("class", "show");
            } else { //else use alternate colors
                element.setAttribute("class", "show huerotate");
            }
            cards[index].index = index;
            selectedCards[selectedCards.length] = cards[index];
            if(selectedCards.length == 2){
                pair = validateCards();
            }

            else if(selectedCards.length == 3){
                if(!pair){
                    resetCards();
                    selectedCards = new Array();

                    return false;
                }
                else{
                    selectedCards = new Array();
                    selectedCards[0] = cards[index];    
                    pair= false;
                }
            }
        }
    //document.getElementById("player1").innerHTML = p1score;
    }
}

function validateCards(){
    tries++;
    if(selectedCards[0].identifier == selectedCards[1].identifier){
        remainingPairs--;
        if(remainingPairs <= 0) { //GAME OVER
            stopGame();
        }
        return true; //found a pair
    }
    return false; //did not find pair
}

function resetCards(){
    for(var i = 0; i < selectedCards.length; i++){
        document.getElementById(selectedCards[i].index).setAttribute("src", "");
        document.getElementById(selectedCards[i].index).setAttribute("class", "card");
    }
}

var pair = false;
var tries = 0;
var remainingPairs = 12;
var selectedCards = new Array();
var images = new Array();
var cards = new Array();

function generate()
{

    images[0] = "./Img/bull.jpg";
    images[1] = "./Img/camel.jpg";
    images[2] = "./Img/cat.jpg";
    images[3] = "./Img/hippo.jpg";
    images[4] = "./Img/croc.jpg";
    images[5] = "./Img/dog.jpg";
    images[6] = "./Img/lion.jpg";
    images[7] = "./Img/monkey.jpg";
    images[8] = "./Img/pig.jpg";
    images[9] = "./Img/rabbit.jpg";
    images[10] = "./Img/sheep.jpg";
    images[11] = "./Img/turtle.jpg";
    images[12] = "./Img/mouse.jpg";

    var i;
    for(i=0;i<24;i++) {
        cards[i] = new Object();
        cards[i].order = Math.random();
        cards[i].identifier = Math.floor(i/2);
    }
    cards.sort(function(a,b){return a.order - b.order});

}

function resetGame()
{
    for(i=0;i<24;i++) {
        document.getElementById(i).setAttribute("src", "");
        document.getElementById(i).setAttribute("class", "card");
    }
    pair = false;
    tries = 0;
    remainingPairs = 12;
    selectedCards = new Array();
    images = new Array();
    cards = new Array();
    generate();
    harmonyMode = !harmonyMode;
}



function addSession(time, tries, isColorSynced) {


    if (isColorSynced) {
        var syncedRef = new Firebase("https://blazing-inferno-8421.firebaseio.com/sessions/colorSyncedSessions");
        syncedRef.push({ "time": time, "tries": tries }, error);    
    }
    else {
        var nonSyncedRef = new Firebase("https://blazing-inferno-8421.firebaseio.com/sessions/NonColorSyncedSessions");
        nonSyncedRef.push({ "time": time, "tries": tries }, error);
           
    }

    function error(err) {
        if (error) {
            console.log("Write Error: " + err);
        }
    }

}