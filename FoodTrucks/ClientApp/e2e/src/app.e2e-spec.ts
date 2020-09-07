import { AppPage } from './app.po';

describe('App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display application title', () => {
    page.navigateTo();
    expect(page.getMainHeading()).toEqual('San Francisco Food Trucks');
  });
});
