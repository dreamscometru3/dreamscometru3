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

    result = [];
    timeoutHandles = [];
    isTestGame = false;
    gameIsFinished = false;
    roundIsFinished = false;
    currentIndex = 0;
    modal = "#modal"

    return {
        init: function (isTest) {
            console.log('logic game');

            isTestGame = isTest;

            this.initEvents();
        },

        initEvents: function () {
            this.displayScreen(currentIndex);
        },

        displayScreen: function (index) {
            $(modal).modal('hide');
            $('.logicImage').attr('src', images[index]);
            this.generateAnsware(index);
            this.setTime(10);
            this.updateGameState();
        },

        generateAnsware: function (index) {
            var scope = this;
            var answare = $(".logicAnsware");
            answare.html('');

            for (var i = 0; i < 8; i++) {
                answare.append( "<div><input class='answareChb' value='" + i + "'type='checkbox'> " +  i + "</div>")
            }

            scope.clearCounting();

            $('.answareChb').bind('click', function () {
                var isChecked = $(this).prop('checked');

                if (isChecked) {
                    value = $(this).attr('value');
                    result.push(value)
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
                this.displayScreen(++currentIndex)
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

            timeoutHandles.push(handler)
        },

        updateGameState: function () {
            var games = result.length;

            if (games >= this.getGameNumber()) {
                gameIsFinished = true;

                if (isTestGame) {
                    alert('Teraz rozpoczniesz grę z przeciwnikiem')
                    window.location.href = "/Game/GameStep3?game=1";
                } else {
                    alert('Teraz rozpoczniesz drugą grę')
                }
            }
        },

        getGameNumber: function () {
            return isTestGame ? 3 : 10;
        },

        showModal: function (text) {
            $(modal).modal({ backdrop: 'static', keyboard: false})
            $(modal).find('.modal-body').text(text);
        },

        clearCounting: function () {
            for (var i = 0; i < timeoutHandles.length; i++) {
                window.clearTimeout(timeoutHandles[i]);
            }
        },

        startNextRound: function () {
            var scope = this;
            var timeWaitingForNextRound = Math.floor(Math.random() * 6) + 4;

            setTimeout(function () {
               scope.displayScreen(++currentIndex)
            }, timeWaitingForNextRound * 1000);
        }
    }
});