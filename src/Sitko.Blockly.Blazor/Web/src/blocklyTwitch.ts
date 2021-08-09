import {TwitchPlayer} from "twitch-player";
import {BlocklyBase} from "./base";

class BlocklyTwitch extends BlocklyBase {
  constructor() {
    super('BlocklyTwitch');
  }

  public render(container: HTMLElement, video?: string, channel?: string, collection?: string) {
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
