import {TwitchPlayer} from "twitch-player";

class BlocklyTwitch {
  private _isLoaded = false;
  private _onReady: Function[] = [];

  private load() {
    let d = document;
    let id = "twitch-js";
    let js, fjs = d.getElementsByTagName('script')[0];
    if (d.getElementById(id)) return;
    js = d.createElement('script');
    js.id = id;
    js.src = "https://player.twitch.tv/js/embed/v1.js";
    fjs.parentNode.insertBefore(js, fjs);
    js.addEventListener('load', () => {
      this._isLoaded = true;
      this._onReady.forEach(request => {
        request();
      });
    });
  }

  public render(container: HTMLElement, video?: string, channel?: string, collection?: string) {
    if (!this._isLoaded) {
      this.load();
      this._onReady.push(() => {
        this.render(container, video, channel, collection);
      });
      return;
    }
    console.debug('render twitch', container, video, channel, collection);
    container.innerHTML = '';
    TwitchPlayer.FromOptions('twitch-player', {
      width: 1280,
      height: 720,
      autoplay: false,
      video: video,
      channel: channel,
      collection: collection
    });
  }

  public clear(container: HTMLElement) {
    container.innerHTML = '';
  }
}

if (!window.Blockly) {
  window.Blockly = {};
}
if (!window.Blockly.Twitch) {
  window.Blockly.Twitch = new BlocklyTwitch();
}
