import { PlayDateTemplatePage } from './app.po';

describe('PlayDate App', function() {
  let page: PlayDateTemplatePage;

  beforeEach(() => {
    page = new PlayDateTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
