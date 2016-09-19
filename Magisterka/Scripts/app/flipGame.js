define(function () {
    result = [];
    timeoutHandles = [];
    isTestGame = false;
    gameIsFinished = false;
    userPoints = 0;
    opponentPoints = 0;
    gameIsFinished = false;
    roundIsFinished = false;
    currentIndex = 0;
    currentGroup = 0;

    return {
        init: function(isTest) {
            console.log('flip game');

            isTestGame = isTest;

            this.hideControls();
            this.initEvents();
        },

        hideControls: function() {
            $('#logicGame').hide();
        },

        initEvents: function() {

            this.setTime(10);

            var scope = this;
            $('.coin-r').bind('click', function() {
                if (!gameIsFinished) {
                    result.push(1);
                    scope.updateGameState(1);

                }
            });

            $('.coin-o').bind('click', function() {
                if (!gameIsFinished) {
                    result.push(0);
                    scope.updateGameState(0);
                }
            });
        },

        finishCountDown: function() {
            alert('Nie wybrałeś odpowiedzi. W normalnej grze będzie to skutkowało utratą punktów.')
            console.log('Finish Countdown');
            currentIndex++;
        },

        setTime: function(count) {
            var scope = this;
            $('.timer').text(count);


            if (gameIsFinished) {
                return;
            }

            var handler = setTimeout(function() {
                $('.timer').text(count);
                count--;

                if (count >= 0) {
                    scope.setTime(count);
                } else {
                    scope.finishCountDown();
                }
            }, 1000);

            timeoutHandles.push(handler);
        },

        updateResult: function() {
            var resultPlaceholder = $('.results');
            resultPlaceholder.html('');

            for (var i = 0; i < timeoutHandles.length; i++) {
                window.clearTimeout(timeoutHandles[i]);
            }

            $(".resultMe").text(userPoints);
            $(".resultOpp").text(opponentPoints);

            this.setTime(10);
        },

        updateGameState: function (selectedAnsware) {
            gameIsFinished = true;

            if (currentIndex >= this.getGameNumber()) {

                if (isTestGame) {
                    alert('Teraz rozpoczniesz drugą grę testową')
                    window.location.href = "/Game/GameStep3?game=0";
                } else {
                    alert('Teraz rozpoczniesz drugą grę');
                }
            }

            if (isTestGame) {
                this.testGameRandomWinner();
            } else {
                this.selectWinner(selectedAnsware);
            }

            currentIndex++;
        },

        selectWinner: function (selectedAnsware) {
            var scope = this;
            if (currentIndex % 2 == 0) {
                var selectedText = selectedAnsware == 0 ? "Wybrałeś reszkę." : "Wybrałeś orła.";
                this.showModal(selectedText);

                setTimeout(function () {
                    scope.calculateReasult();
                    scope.updateResult();
                    $(modal).modal('hide');

                    scope.goToOpponentRound();
                }, 2000);

            } else {
                var selectedText = selectedAnsware == 0 ? "Twój przeciwnik wybrał reszkę." : "Twój przeciwnik wybrał orła.";
                this.showModal(selectedText);

                scope.calculateReasult();
                scope.updateResult();
                $(modal).modal('hide');
            }
        },

        goToUserRound: function() {
            this.setTime(10);
        },

        goToOpponentRound: function () {
            var scope = this;

            scope.showModal();
            scope.showModal("Teraz wybiera Twój przeciwnik.");
           
            setTimeout(function () {
                scope.selectWinner(Math.round(Math.random()));
            }, 4000);
        },

        calculateReasult: function() {
            var i = Math.round(Math.random());

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

        getGameNumber: function() {
            return isTestGame ? 3 : 10;
        },

        testGameRandomWinner: function() {
            var oppoentWin = Math.round(Math.random());

            if (oppoentWin) {
                opponentPoints++;
            } else {
                userPoints++;
            }
        },

        showModal: function(text) {
            $(modal).modal({ backdrop: 'static', keyboard: false });
            $(modal).find('.modal-body').text(text);
        },
        chooseNewGroup: function() {
            if (failedAnwserd == 1 && currentGroup == 0) {
                currentGroup = 1;
            } else if (failedAnwserd > 1 && currentGroup == 1) {
                currentGroup = 2;
            }
        }
    }
});