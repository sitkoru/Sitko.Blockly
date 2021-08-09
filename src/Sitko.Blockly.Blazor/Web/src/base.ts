import * as log from "loglevel";

export abstract class BlocklyBase {
  private logger: log.Logger;

  protected constructor(protected name: string) {
    this.logger = log.getLogger(this.name);
    this.logger.setLevel(process.env.loglevel as log.LogLevelDesc ?? 'debug');
  }

  info(...msg: any[]): void {
    this.logger.info(this.name, msg);
  }

  debug(...msg: any[]): void {
    this.logger.debug(this.name, msg);
  }

  error(...msg: any[]): void {
    this.logger.error(this.name, msg);
  }

  warn(...msg: any[]): void {
    this.logger.warn(this.name, msg);
  }
}
