import './index.scss';

window.Blockly = {
    Twitter: {
        load: function () {
            let d = document;
            let s = "script";
            let id = "twitter-wjs";
            let js, fjs = d.getElementsByTagName(s)[0],
                t = window.twttr || {};
            if (d.getElementById(id)) return t;
            js = d.createElement(s);
            js.id = id;
            js.src = "https://platform.twitter.com/widgets.js";
            fjs.parentNode.insertBefore(js, fjs);
            t._e = [];
            t.ready = function (f) {
                t._e.push(f);
            };
            return t;
        },
        render: function (params) {
            console.log('render tweet', params);
            window.twttr = Blockly.Twitter.load();
            window.twttr.ready(function (tw) {
                console.debug("Twitter is ready");
                const container = document.getElementById('twitter-' + params.id);
                container.innerHTML = '';
                tw.widgets.createTweet(
                    params.tweetId + '', container,
                    {
                        theme: 'dark'
                    }
                );
            });
        },
        clear: function (params) {
            const container = document.getElementById('twitter-' + params.id);
            container.innerHTML = '';
        }
    },
    Twitch: {
        isLoaded: false,
        onReady: [],
        load: function () {
            let d = document;
            let s = "script";
            let id = "twitch-js";
            let js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s);
            js.id = id;
            js.src = "https://player.twitch.tv/js/embed/v1.js";
            fjs.parentNode.insertBefore(js, fjs);
            js.addEventListener('load', function () {
                Blockly.Twitch._isLoaded = true;
                Blockly.Twitch.onReady.forEach(video => {
                    Blockly.Twitch.render(video);
                });
            });
        },
        render: function (params) {
            if (!Blockly.Twitch._isLoaded) {
                Blockly.Twitch.onReady.push(params);
                return;
            }
            console.log('render twitch', params);
            const container = document.getElementById('twitch-' + params.id);
            container.innerHTML = '';
            const twitchParams = {
                width: 854,
                height: 480,
                layout: 'video',
                autoplay: false
            };
            if (params.video) {
                twitchParams.video = params.video;
            } else if (params.channel) {
                twitchParams.channel = params.channel;
            } else if (params.collection) {
                twitchParams.collection = params.collection;
            }
            new Twitch.Player('twitch-' + params.id, twitchParams);
        },
        clear: function (params) {
            const container = document.getElementById('twitch-' + params.id);
            container.innerHTML = '';
        }
    }
}
