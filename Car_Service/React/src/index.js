import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { createStore, applyMiddleware} from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension' ;
import createSagaMiddleware from 'redux-saga';

import App from './containers/app.js';

import rootReducer from './reducers';
import rootSaga from './saga'

const sagaMiddleware = createSagaMiddleware();
const store = createStore(rootReducer, composeWithDevTools(applyMiddleware(sagaMiddleware)) );

sagaMiddleware.run(rootSaga);

export default App;

ReactDOM.render(
    <Provider store={store}>
        <App/>
    </Provider>,
    document.getElementById('app'));