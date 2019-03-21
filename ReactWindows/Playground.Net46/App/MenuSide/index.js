import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { View } from 'react-native';
import styles from './styles'

import { Pages } from '../ContentSide/constant'

import MenuButton from './MenuButton'

export default class MenuSide extends Component {
  static propTypes = {
    logger: PropTypes.func,
    menuClick: PropTypes.func
  }

  menuClick = (page) => {
    this.props.menuClick(page)
  }

  render() {
    return (
      <View style={styles.content}>
        <MenuButton caption={Pages.CONTROLS} onClick={this.menuClick.bind(null, Pages.CONTROLS)} />
        <MenuButton caption={Pages.EVENTS} onClick={this.menuClick.bind(null, Pages.EVENTS)} />
      </View>
    )
  }
}