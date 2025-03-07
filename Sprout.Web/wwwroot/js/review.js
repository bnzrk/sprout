$(function () {

    let fetchCount = 0; // test

    var cardWrapper = $("#swipe-card");
    var flipCard = $("#flippable");
    var reviewMenu = $("#review-menu");

    let cardsExhausted = false;
    let reviewQueue = [];
    let learningQueue = [];
    let currentCard = null;
    let revealedCurrentCard = false;
    // init deck id as null, get from query param if it exists

    let startX = 0, currentX = 0, isDragging = false, hasClicked = false, hasMoved = false, starTime = 0, velocity = 0;

    init();

    $("#correct-button").on("click", function () { submitReview(true); });
    $("#incorrect-button").on("click", function () { submitReview(false); });
    flipCard.on("click", function () {
        if (!(Math.abs(currentX - startX) > 0)) {
            if (currentCard === null)
                return;

            if (!revealedCurrentCard && !($(this).hasClass("flipped")))
                revealCurrentCard();
            $(this).toggleClass("flipped");
        }
    });

    function revealCurrentCard() {
        revealedCurrentCard = true;
        reviewMenu.addClass("revealed");
    }

    function startDrag(e) {
        e.preventDefault();
        startX = e.touches ? e.touches[0].clientX : e.clientX;
        currentX = startX;
        hasClicked = true;
        isDragging = false;
        hasMoved = false;
        cardWrapper.css("transition", "none");
    }

    function moveDrag(e) {
        if (!hasClicked)
            return;
        currentX = e.touches ? e.touches[0].clientX : e.clientX;
        let moveX = currentX - startX;
        if (Math.abs(moveX) > 1)
            isDragging = true;
        cardWrapper.css("transform", `translateX(${moveX}px)`);
        if (Math.abs(moveX) > 5) hasMoved = true;
    }

    function endDrag() {
        if (!hasMoved) return;
        isDragging = false;
        hasClicked = false;
        let moveX = currentX - startX;

        // Over threshhold, dismiss
        if (Math.abs(moveX) > 50) {
            cardWrapper.css({
                'transition': "transform 0.3s ease-out, opacity 0.2s ease-out",
                'opacity': "0"
            });
        }
        // Under threshold, snap back 
        else {
            // Reset position if swipe is not enough
            cardWrapper.css({
                'transition': "transform 0.3s ease-out, opacity 0.3s ease-out",
                'transform': "translateX(0)",
                'opacity': "1"
            });
        }
        moveX = 0;
    }

    function updateCardDisplay() {
        revealedCurrentCard = false;
        flipCard.addClass("no-transition").removeClass("flipped");
        setTimeout(() => {
            flipCard.removeClass("no-transition");
        }, 100);

        // Clear card elements
        var curr = currentCard;
        $("#meanings, #front-kanji, #back-kanji, #kun-readings, #on-readings").html("");

        if (currentCard == null) {
            cardWrapper.css("visibility", "hidden");
            return;
        }
        else {
            cardWrapper.css("visibility", "visible");
        }

        // Populate card elements
        $("#front-kanji, #back-kanji").text(curr.literal);

        curr.kunReadings.forEach((element) => {
            $("#kun-readings").append(`<p class="font-noto tag white-color alt-accent-bg reading">${element}</p>`);
        });
        curr.onReadings.forEach((element) => {
            $("#on-readings").append(`<p class="font-noto tag white-color alt-accent-bg reading">${element}</p>`);
        });

        curr.meanings.forEach((element) => {
            $("#meanings").append(`<p class="tag white-color accent-bg">${element}</p>`);
        });
    }

    /* REVIEW FUNCTIONS */
    function Card(literal, onReadings, kunReadings, meanings) {
        this.literal = literal;
        this.onReadings = onReadings;
        this.kunReadings = kunReadings;
        this.meanings = meanings;
    }

    function fetchReviewCards() {
        if (fetchCount > 1) {
            cardsExhausted = true;
            return;
        }

        $.ajax({
            url: 'http://localhost:5152/api/v1/cards/due',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                onCardsFetched(response);
            }
        });

        fetchCount++;
    }

    function onCardsFetched(cards) {
        // Request kanji details for cards
        var kanjiLiterals = [];
        cards.forEach(c => {
            kanjiLiterals.push(c.kanji);
        });

        $.ajax({
            url: 'http://localhost:5152/api/v1/kanji/batch',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(kanjiLiterals),  
            success: function (response) {
                let merged = cards.map(card => {
                    console.log(card);
                    let kanjiMatch = response.find(k => k.literal == card.kanji);
                    console.log(kanjiMatch);
                    return new Card(card.kanji, kanjiMatch.onReadings, kanjiMatch.kunReadings, kanjiMatch.meanings);
                });
                console.log(merged);
                reviewQueue = reviewQueue.concat(merged);
                console.log(reviewQueue);
                nextCard();
            }
        });
    }

    function submitReview(isCorrect) {
        reviewMenu.removeClass("revealed");
        if (!isCorrect) {
            learningQueue.push(currentCard);
        }
        nextCard();
        updateCardDisplay();

        //TODO: create review object and send http post
    }

    // Moves to the next card to review and updates the card display
    function nextCard() {
        if (reviewQueue.length > 0) {
            currentCard = reviewQueue[0];
            reviewQueue.shift(1);
        }
        else if (learningQueue.length > 0) {
            currentCard = learningQueue[0];
            learningQueue.shift(1);
        }
        else
            currentCard = null;

        //if (!cardsExhausted) {
        //    var count = fetchReviewCards();
        //}
        updateCardDisplay();
    }

    function init() {
        fetchReviewCards();
    }
});