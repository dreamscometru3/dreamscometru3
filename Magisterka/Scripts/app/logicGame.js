define(function () {
    images = [
        "/Images/game/1.jpg",
        "/Images/game/2.jpg",
        "/Images/game/3.jpg",
        "/Images/game/4.jpg",
        "/Images/game/5.jpg",
        "/Images/game/6.jpg",
        "/Images/game/7.jpg",
        "/Images/game/8.jpg",
        "/Images/game/9.jpg",
        "/Images/game/10.jpg"];

    imageAnsware = [
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1
    ];
    userPoints = 0;
    opponentPoints = 0;
    failedAnwserd = 0;
    currentGroup = 0;
    result = [];
    timeoutHandles = [];
    isTestGame = false;
    gameIsFinished = false;
    roundIsFinished = false;
    currentIndex = 0;
    modal = "#modal";

    return {
        init: function (isTest) {
            console.log('logic game');

            isTestGame = isTest;
            this.selectGroup();
            this.initEvents();
            
        },
        selectGroup: function () {
            var randomGroup = Math.round(Math.random() * 2);
            currentGroup = randomGroup;
        },

        initEvents: function () {
            this.displayScreen(currentIndex);
        },

        displayScreen: function (index) {
            $(modal).modal('hide');
            $('.logicImage').attr('src', images[index]);
            this.generateAnsware(index);
            this.setTime(3);
            this.updateGameState();
        },

        generateAnsware: function (index) {
            var scope = this;
            var answare = $(".logicAnsware");
            answare.html('');

            for (var i = 1; i < 9; i++) {
                answare.append( "<div><input class='answareChb' value='" + i + "'type='checkbox'> " +  i + "</div>")
            }

            scope.clearCounting();

            $('.answareChb').bind('click', function () {
                var isChecked = $(this).prop('checked');

                if (isChecked) {
                    value = $(this).attr('value');
                    result.push(value);
                }

                scope.SelectAnsware();
            });
        },

        SelectAnsware: function() {
            if (!isTestGame) {
                roundIsFinished = true;
                this.showModal('Sprawdzanie odpowiedzi oraz wyłonienie zwycięzcy... zaraz ropocznie się kolejna runda')
                this.clearCounting();
                this.startNextRound();
            } else {
                this.displayScreen(++currentIndex);
            }

            console.log(result);
        },

        finishCountDown: function () {
            if (isTestGame) {
                alert('Nie wybrałeś odpowiedzi. W normalnej grze będzie to skutkowało utratą punktów.')
            } else {
                result.push("Brak odpowiedzi");
                this.SelectAnsware();
            }
        },

        setTime: function (count) {
            var scope = this;
            $('.timerLogic').text(count);

            if (gameIsFinished) {
                return;
            }

            var handler = setTimeout(function () {
                $('.timerLogic').text(count);
                count--;

                if (count >= 0) {
                    scope.setTime(count);
                } else {
                    scope.finishCountDown();
                }
            }, 1000);

            timeoutHandles.push(handler);
        },

        updateGameState: function () {
            var games = result.length;

            if (games >= this.getGameNumber()) {
                gameIsFinished = true;

                if (isTestGame) {
                    alert('Teraz rozpoczniesz grę z przeciwnikiem');
                    window.location.href = "/Game/GameStep3?game=1";
                } else {
                    alert('Teraz rozpoczniesz drugą grę');
                }
            }
        },

        getGameNumber: function () {
            return isTestGame ? 3 : 10;
        },

        showModal: function (text) {
            $(modal).modal({ backdrop: 'static', keyboard: false });
            $(modal).find('.modal-body').text(text);
        },

        clearCounting: function () {
            for (var i = 0; i < timeoutHandles.length; i++) {
                window.clearTimeout(timeoutHandles[i]);
            }
        },

        startNextRound: function () {
            var scope = this;
            var timeWaitingForNextRound = 1;// Math.round(Math.random() * 6) + 4;


            setTimeout(function () {
                scope.calculateReasult();
                scope.displayResults();
                scope.displayScreen(++currentIndex);
            }, timeWaitingForNextRound * 1000);
        },
        displayResults: function () {
            $('.resLogicMe').text(userPoints);
            $('.resLogicOpponent').text(opponentPoints);
        },
        calculateReasult: function () {
            var userAnwser = result[currentIndex];
            var correctAnwser = imageAnsware[currentIndex];
            var i = userAnwser == correctAnwser ? 1 : 0;

           
            if (currentGroup == 0) {
                if (i == 1) {
                    userPoints++;
                }

                if (i == 0) {
                    currentGroup = 1;
                }
            }

            if (currentGroup == 1) {
                if (i == 1) {
                    var opponentWin = Math.round(Math.random());

                    if (opponentWin == 1 && opponentPoints == 0) {
                        opponentPoints++;
                        return;
                    }

                    if (userPoints == 4 && opponentPoints == 0) {
                        opponentPoints++;
                        return;
                    }

                    if (opponentPoints > 1) {
                        currentGroup = 2;
                        return;
                    }

                    userPoints++;
                }

                if (i == 0) {
                    if (opponentPoints == 0 || opponentPoints == 1) {
                        opponentPoints++;
                    }
                }

                if (opponentPoints > 1) {
                    currentGroup = 2;
                    return;
                }
            }

            if (currentGroup == 2) {
                if (i == 1) {
                    var opponentWin = Math.round(Math.random());

                    if (opponentWin == 1 && opponentPoints < 4) {
                        opponentPoints++;
                        return;
                    }

                    if (userPoints == 4 && opponentPoints < 4) {
                        opponentPoints++;
                        return;
                    }

                    userPoints++;
                }

                if (i == 0) {

                    if (opponentPoints <= 4) {
                        opponentPoints++;
                    }
                }
            }

            if (opponentPoints == 5) {
                alert("przegrales!");
                window.location.href = "/Game/GameStep4?result=2";
            }

            if (userPoints == 5) {
                alert("Jebako zwyciezylees!");
                window.location.href = "/Game/GameStep4?result=1";
            }

            this.chooseNewGroup();
        },
        chooseNewGroup: function () {
            if (failedAnwserd == 1 && currentGroup == 0) {
                currentGroup = 1;
            } else if (failedAnwserd > 1 && currentGroup == 1) {
                currentGroup = 2;
            }
        }
    }
});