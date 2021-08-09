import {TwitchPlayer} from "twitch-player";
import {BlocklyBase} from "./base";

class BlocklyTwitch extends BlocklyBase {
  private _isLoaded = false;
  private _onReady: Function[] = [];

  constructor() {
    super('BlocklyTwitch');
  }

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
    this.debug('render twitch', container, video, channel, collection);
    container.innerHTML = '';
    TwitchPlayer.FromOptions(container.id, {
      width: 640,
      height: 360,
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
