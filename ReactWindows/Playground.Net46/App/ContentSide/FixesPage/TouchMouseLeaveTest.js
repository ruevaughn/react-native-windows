import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native'
import styles from './styles'

export default class TouchMouseLeaveTest extends Component {
  static propTypes = {
    logger: PropTypes.func
  }

  constructor(props) {
    super(props)
  }

  log = (msg) => {
    this.props.logger(`[TouchMouseLeaveTest] ${msg}`)
  }

  render() {
    return (
      <View style={styles.content}>
        <Text selectable={true} accessibilityLabel={'Clicking Touchable Erroneously Fires onMouseLeave Event'} style={styles.subCaption}>Clicking Touchable Erroneously Fires onMouseLeave Event</Text>
        <View
          onMouseEnter={this.log.bind(null, 'hit onMouseEnter')}
          onMouseLeave={this.log.bind(null, 'hit onMouseLeave')}
          style={{ height: 30, backgroundColor: 'white' }}>
          <View isFocusable={true} accessibilityLabel={'Click Me!'}>
          <TouchableOpacity
            onPress={this.log.bind(null, 'clicked!')}
          >
            <Text>Click Me!</Text>
          </TouchableOpacity>
          </View>
        </View>
      </View>
    )
  }
}
