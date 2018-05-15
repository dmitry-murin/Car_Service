import { connect } from 'react-redux'

import App from '../components/App/index.jsx';
import {initUser} from '../actions';

const mapStateToProps = state => ({
  user: state.app.user
})

const mapDispatchToProps = dispatch => ({
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(App)