import { TimelapsePage } from './app.po';

describe('timelapse App', () => {
  let page: TimelapsePage;

  beforeEach(() => {
    page = new TimelapsePage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
